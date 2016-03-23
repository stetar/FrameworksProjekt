using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    public class Tooltip
    {
        private Rectangle rectangle;
        private string text;
        private Color backgroundColor;
        private Color textColor;
        private Vector2 textPosition;
        private Texture2D rect;
        private SpriteFont font;

        public Tooltip(Rectangle rectangle, string text, Vector2 textRelativePosition, Color backgroundColor, Color textColor)
        {
            this.rectangle = rectangle;
            this.text = text;
            this.backgroundColor = backgroundColor;
            this.textColor = textColor;
            this.textPosition = new Vector2(rectangle.X, rectangle.Y) + textRelativePosition;
            this.font = GameWorld.Instance.StandardFont;

            rect = new Texture2D(GameWorld.Instance.Graphics.GraphicsDevice, Rectangle.Width, Rectangle.Height);
            Color[] data = new Color[Rectangle.Width * Rectangle.Height];
            for (int i = 0; i < data.Length; i++) data[i] = backgroundColor;
            rect.SetData(data);
        }

        public Rectangle Rectangle
        {
            get
            {
                return rectangle;
            }

            set
            {
                rectangle = value;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {           
            spriteBatch.Draw(rect, new Vector2(Rectangle.X, Rectangle.Y) - GameWorld.Instance.Camera.Position, backgroundColor);
            spriteBatch.DrawString(font, text, textPosition - GameWorld.Instance.Camera.Position, textColor);
        }
    }
}
