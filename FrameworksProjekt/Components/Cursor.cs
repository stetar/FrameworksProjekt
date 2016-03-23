using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FrameworksProjekt
{
    public class Cursor : Component, IUpdateable, ILoadable, IOnCollisionStay
    {
        private Texture2D sprite;
        private Vector2 position;
        private Rectangle rectangle;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Cursor(GameObject gameObject) : base(gameObject)
        {
        }

        public void LoadContent(ContentManager content)
        {
            sprite = ((SpriteRenderer)GameObject.GetComponent("SpriteRenderer")).Sprite;
            ((SpriteRenderer)GameObject.GetComponent("SpriteRenderer")).Rectangle = new Rectangle(0, 0, sprite.Width, sprite.Height);
            position = GameObject.GetTransform.Position;
            Rectangle = new Rectangle((int)this.position.X - sprite.Width/2, (int)this.position.Y - sprite.Height/2, sprite.Width, sprite.Height);
        }

        public void Update()
        {
            position = new Vector2(Mouse.GetState().X - sprite.Width / 2, Mouse.GetState().Y - sprite.Width / 2);
            this.GameObject.GetTransform.Position = position;
#if DEBUG
            System.Diagnostics.Debug.WriteLine(position.X + ", "+position.Y);
#endif
        }

        public void OnCollisionStay(Collider other)
        {
            
        }

    }
}
