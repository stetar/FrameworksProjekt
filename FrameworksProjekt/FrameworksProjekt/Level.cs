using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    public class Level
    {
        private Texture2D background;
        private string imageString;
        // distance from left and right wall that player stops
        private Tuple<int, int> boundaries;
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

        public Tuple<int, int> Boundaries
        {
            get
            {
                return boundaries;
            }

            set
            {
                boundaries = value;
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

        public Level(string imageString, Tuple<int, int> boundaries)
        {
            this.imageString = imageString;
            this.InterestPoints = new Dictionary<Rectangle, Action>();
            this.Boundaries = boundaries;
        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(imageString);

            this.boundaries = new Tuple<int, int>(boundaries.Item1, background.Width - this.boundaries.Item2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle((int)GameWorld.Instance.Camera.Position.X, (int)GameWorld.Instance.Camera.Position.Y, GameWorld.Instance.DisplayRect.Width, GameWorld.Instance.DisplayRect.Height);

            spriteBatch.Draw(background, new Rectangle(0, 0, GameWorld.Instance.DisplayRect.Width, GameWorld.Instance.DisplayRect.Height), sourceRect, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }
    }
}
