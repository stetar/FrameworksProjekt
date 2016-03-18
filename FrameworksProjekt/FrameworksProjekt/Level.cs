using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    public abstract class Level
    {
        private Texture2D background;
        private string imageString;
        // Actions to do if collision with given interest-point
        private Dictionary<Rectangle, Action> interestPoints;

        bool test;

        public int Width
        {
            get
            {
                return background.Width;
            }
        }

        public int Height
        {
            get
            {
                return background.Height;
            }
        }

        public Dictionary<Rectangle, Action> InterestPoints
        {
            get
            {
                return interestPoints;
            }

            set
            {
                interestPoints = value;
            }
        }

        public Level(string imageString)
        {
            this.imageString = imageString;
            this.InterestPoints = new Dictionary<Rectangle, Action>();
        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(imageString);
        }

        public abstract void CreateInterestPoints();

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle((int)GameWorld.Instance.Camera.Position.X, (int)GameWorld.Instance.Camera.Position.Y, GameWorld.Instance.DisplayRect.Width, GameWorld.Instance.DisplayRect.Height);

            spriteBatch.Draw(background, Vector2.Zero, sourceRect, Color.White);
        }
    }
}
