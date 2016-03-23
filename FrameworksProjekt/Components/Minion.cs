using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FrameworksProjekt.Components
{
    public enum Miniontype
    {
        Chubby,
        Weeaboo,
        Hipster,

    }
    public class Minion : Component, ILoadable, IUpdateable
    {
        private float speed;
        private float strength;
        public Animator animator;
        private Direction direction;
        private Miniontype type;
        private IStrategy strategy;
        private Vector2 target;
        private Level currentLevel;
        private static Random r = new Random();

        public Miniontype Type
        {
            get { return type; }
            set { type = value; }
        }

        public Level CurrentLevel
        {
            get
            {
                return currentLevel;
            }

            set
            {
                currentLevel = value;
            }
        }

        public Minion(GameObject gameObject) : base(gameObject)
        {
            this.direction = Direction.Down;
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator) GameObject.GetComponent("Animator");
            this.strategy = new Idle(animator);

            SetTypeValues();
            CreateAnimation();
        }

        public void Update()
        {
            Move();
        }

        public void Move()
        {
            Vector2 translation = Vector2.Zero;

            if(Math.Abs(target.X - this.GameObject.GetTransform.Position.X) < 1)
            {
                target = Vector2.Zero;
            }

            if(target != Vector2.Zero)
            { 
                if (this.GameObject.GetTransform.Position.X > target.X)
                {
                    translation += new Vector2(-1,0);
                    direction = Direction.Left;
                    animator.PlayAnimation("WalkLeft");
                }
                else if (this.GameObject.GetTransform.Position.X < target.X)
                {
                    translation += new Vector2(1,0);
                    direction = Direction.Right;
                    animator.PlayAnimation("WalkRight");
                }
            }
            else
            {
                animator.PlayAnimation("IdleFront");

                if(r.Next(1000) <= 1)
                {
                    target = new Vector2(r.Next(currentLevel.Boundaries.Item1, currentLevel.Boundaries.Item2 - ((SpriteRenderer)GameObject.GetComponent("SpriteRenderer")).Rectangle.Width), this.GameObject.GetTransform.Position.Y);
                }
            }

            UpdateAnimation();

            GameObject.GetTransform.Translate(translation * speed * GameWorld.Instance.Delta);
        }

        public void UpdateAnimation()
        {
            
        }

        public void CreateAnimation()
        {
            animator.CreateAnimation("IdleFront", new Animation(2, 0, 0, 128, 128, 2, new Vector2(0, 0)));
            animator.CreateAnimation("WalkRight", new Animation(2, 128, 0, 128, 128, 4, new Vector2(0, 0)));
            animator.CreateAnimation("WalkLeft", new Animation(2, 256, 0, 128, 128, 4, new Vector2(0, 0)));

            animator.PlayAnimation("IdleFront");
        }

        public void SetTypeValues()
        {
            switch (type)
            {
                    case Miniontype.Chubby:
                    strength = 10;
                    speed = 70;
                    return;
                    
                    case Miniontype.Hipster:
                    strength = 7;
                    speed = 90;
                    return;

                    case Miniontype.Weeaboo:
                    strength = 3;
                    speed = 110;
                    return;

            }
        }
    }
}
