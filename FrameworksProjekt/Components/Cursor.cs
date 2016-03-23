using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FrameworksProjekt
{
    public class Cursor : Component, IUpdateable, ILoadable
    {
        private Texture2D sprite;
        private Point position;
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
            sprite = content.Load<Texture2D>("Hook");
            Rectangle = new Rectangle(this.position.X, this.position.Y, sprite.Width, sprite.Height);
        }

        public void Update()
        {
            position = new Point(Mouse.GetState().X, Mouse.GetState().Y);
        }

    }
}
