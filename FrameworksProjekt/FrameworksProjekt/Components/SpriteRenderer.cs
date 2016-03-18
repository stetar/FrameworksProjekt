using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrameworksProjekt
{
    public class SpriteRenderer : Component, ILoadable, IDrawable
    {
        private Rectangle rectangle;
        private Texture2D sprite;
        private string spriteName;
        private float layerDepth;
        private SpriteEffects effects;
        private Vector2 offset;
        private Color color;

        public Rectangle Rectangle
        {
            get { return rectangle; }
            set { rectangle = value; }
        }

        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Vector2 Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        public SpriteRenderer(GameObject gameObject, string spriteName, float layerDepth, Color color)
            : base(gameObject)
        {
            this.spriteName = spriteName;
            this.layerDepth = layerDepth;
            Color = color;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, GameObject.GetTransform.Position + offset - GameWorld.Instance.Camera.Position, rectangle, color, 0, Vector2.Zero, 1, effects, layerDepth);
        }

        public void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(spriteName);
        }
    }
}