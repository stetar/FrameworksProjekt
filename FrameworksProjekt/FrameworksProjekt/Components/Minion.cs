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


        public Miniontype Type
        {
            get { return type; }
            set { type = value; }
        }

        public Minion(GameObject gameObject) : base(gameObject)
        {
            direction = Direction.Down;
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
            if (target != null)
            {
                Move();
            }

        }

        public void Move()
        {
            Vector2 translation = Vector2.Zero;
            if (this.GameObject.GetTransform.Position.X > target.X)
            {
                translation += new Vector2(-1,0);
                direction = Direction.Left;
            }
            else if (this.GameObject.GetTransform.Position.X < target.X)
            {
                translation += new Vector2(1,0);
                direction = Direction.Right;
            }
            else if (this.GameObject.GetTransform.Position.X == target.X)
            {
                target = new Vector2();
            }

            UpdateAnimation();

            GameObject.GetTransform.Translate(translation * speed * GameWorld.Instance.Delta);
        }

        public void UpdateAnimation()
        {
            
        }

        public void CreateAnimation()
        {
            
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
