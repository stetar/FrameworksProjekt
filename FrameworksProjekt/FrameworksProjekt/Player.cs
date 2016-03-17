﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt
{
    enum Direction
    {
        Up, 
        Down, 
        Left, 
        Right
    }

    class Player : Component, ILoadable, IUpdateable
    {
        private float speed = 100;
        KeyboardState ks;
        public Animator animator;
        private Direction direction;
        private IStrategy strategy;

        public Player(GameObject gameObject) : base(gameObject)
        {
            direction = Direction.Down;
        }

        public void LoadContent(ContentManager content)
        {
            this.animator = (Animator)GameObject.GetComponent("Animator");
            this.strategy = new Idle(animator);

            CreateAnimations();
        }

        public void Update()
        {
            Move();
        }

        public void Move()
        {
            ks = Keyboard.GetState();
            Vector2 translation = Vector2.Zero;

            // Left
            if(ks.IsKeyDown(Keys.A))
            {
                translation += new Vector2(-1, 0);
                direction = Direction.Left;
            }
            else if(ks.IsKeyDown(Keys.D))
            {
                translation += new Vector2(1, 0);
                direction = Direction.Right;
            }

            UpdateAnimation();

            GameObject.GetTransform.Translate(translation * speed * GameWorld.Instance.delta);
        }

        public void UpdateAnimation()
        {

        }

        public void CreateAnimations()
        {
            animator.CreateAnimation("IdleFront", new Animation(4, 0, 0, 128, 128, 4, new Vector2(0, 0)));

            animator.PlayAnimation("IdleFront");
        }
    }
}
