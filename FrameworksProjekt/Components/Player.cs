using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using FrameworksProjekt.Factories;
using FrameworksProjekt.Interfaces;
using FrameworksProjekt.Items;

namespace FrameworksProjekt
{
    enum Direction
    {
        Down, 
        Left, 
        Right
    }

    public class Player : Component, ILoadable, IUpdateable, IOnCollisionStay
    {
        private float speed = 400;
        KeyboardState ks;
        public Animator animator;
        private Direction direction;
        private IStrategy strategy;
        private Inventory inventory;

        CategoryFac cs = new CategoryFac();

        public Player(GameObject gameObject) : base(gameObject)
        {
            direction = Direction.Down;
            inventory = new Inventory();
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

            UpdateCamera();
        }

        public void Move()
        {
            ks = Keyboard.GetState();
            Vector2 translation = Vector2.Zero;

            //Left
            if(ks.IsKeyDown(Keys.A))
            {
                translation += new Vector2(-1, 0);
                direction = Direction.Left;
                animator.PlayAnimation("WalkLeft");
            }

            //Right
            else if(ks.IsKeyDown(Keys.D))
            {
                translation += new Vector2(1, 0);
                direction = Direction.Right;
                animator.PlayAnimation("WalkRight");
            }
            else
            {
                animator.PlayAnimation("IdleFront");
            }

            UpdateAnimation();

            // Move player
            GameObject.GetTransform.Translate(translation * speed * GameWorld.Instance.Delta);
            // Account for level boundaries
            CheckLevelBoundaries(GameObject.GetTransform.Position.X);
        }

        public void CheckLevelBoundaries(float playerX)
        {
            Tuple<int, int> b = GameWorld.Instance.GameLevel.Boundaries;
            int playerWidth = ((SpriteRenderer)GameObject.GetComponent("SpriteRenderer")).Rectangle.Width;

            // right wall
            if (playerX + playerWidth > b.Item2)
            {
                GameObject.GetTransform.Position = new Vector2(b.Item2 - playerWidth, GameObject.GetTransform.Position.Y);
            }
            // left wall
            else if(playerX < b.Item1)
            {
                GameObject.GetTransform.Position = new Vector2(b.Item1, GameObject.GetTransform.Position.Y);
            }
        }

        public void UpdateAnimation()
        {
            
        }

        public void CreateAnimations()
        {
            animator.CreateAnimation("IdleFront", new Animation(4, 0, 0, 128, 128, 4, new Vector2(0, 0)));
            animator.CreateAnimation("WalkRight", new Animation(4, 128, 0, 128, 128, 8, new Vector2(0, 0)));
            animator.CreateAnimation("WalkLeft", new Animation(4, 256, 0, 128, 128, 8, new Vector2(0, 0)));

            animator.PlayAnimation("IdleFront");
        }

        public void UpdateCamera()
        {
            Transform t = GameObject.GetTransform;
            SpriteRenderer s = (SpriteRenderer)this.GameObject.GetComponent("SpriteRenderer");

            int x = (int)(t.Position.X + s.Rectangle.Width / 2 - GameWorld.Instance.DisplayRect.Width / 2);
            int y = (int)(t.Position.Y + s.Rectangle.Height / 2 - GameWorld.Instance.DisplayRect.Height + s.Rectangle.Height + 100);

            // if player is approaching left border of level
            if (x < 0)
            {
                x = 0;
            }               
            // if player is approaching right border of level
            else if(x > GameWorld.Instance.GameLevel.Width - GameWorld.Instance.DisplayRect.Width)
            {
                x = GameWorld.Instance.GameLevel.Width - GameWorld.Instance.DisplayRect.Width;
            }

            if(y < 0)
            {
                y = 0;
            }
            else if(y > GameWorld.Instance.GameLevel.Height - GameWorld.Instance.DisplayRect.Height)
            {
                y = GameWorld.Instance.GameLevel.Height - GameWorld.Instance.DisplayRect.Height;
            }

            GameWorld.Instance.Camera.Position = new Vector2(x, y);
        }

        public void OnCollisionStay(Collider other)
        {

        }
    }
}
