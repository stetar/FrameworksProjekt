using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    class Animator : Component, ILoadable, IUpdateable
    {
        private SpriteRenderer spriteRenderer;
        private int currentIndex;
        private float timeElapsed;
        private float fps;
        private Rectangle[] rectangles;
        private string animationName;
        private Dictionary<string, Animation> animations;

        public string AnimationName
        {
            get
            {
                return animationName;
            }
        }

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
        }

        public Dictionary<string, Animation> Animations
        {
            get
            {
                return animations;
            }
        }

        public Animator(GameObject gameObject) : base(gameObject)
        {
            animations = new Dictionary<string, Animation>();
            fps = 5;
            this.spriteRenderer = (SpriteRenderer)GameObject.GetComponent("SpriteRenderer");
        }

        public void Update()
        {
            timeElapsed += GameWorld.Instance.Delta;

            currentIndex = (int)(timeElapsed * fps);
            
            if(currentIndex >= rectangles.Length)
            {
                timeElapsed = 0;
                currentIndex = 0;
                //GameObject.OnAnimationDone(animationName);
            }
        }

        public void CreateAnimation(string animationName, Animation animation)
        {
            animations.Add(animationName, animation);
        }

        public void PlayAnimation(string animationName)
        {
            if(this.animationName != animationName)
            {
                this.rectangles = animations[animationName].Rectangles;

                this.spriteRenderer.Rectangle = rectangles[0];

                this.spriteRenderer.Offset = animations[animationName].Offset;

                this.animationName = animationName;

                timeElapsed = 0;

                currentIndex = 0;
            }
        }

        public void LoadContent(ContentManager content)
        {

        }
    }
}
