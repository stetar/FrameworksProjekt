using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    abstract class Level
    {
        private Texture2D background;
        private string imageString;
        // Actions to do if collision with given interest-point
        private Dictionary<Vector2, Action> interestPoints;

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

        public Level(string imageString)
        {
            this.imageString = imageString;
        }

        public void LoadContent(ContentManager content)
        {
            background = content.Load<Texture2D>(imageString);
        }

        public abstract void CreateInterestPoints();
    }
}
