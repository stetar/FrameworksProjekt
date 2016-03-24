using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    public enum City
    {
        Aarhus,
        Esbjerg, 
        Skagen,
        København,
        Grenaa
    }

    public class Level
    {
        private Texture2D background;
        private string imageString;
        private string name;
        private Vector2 spawnPoint;
        // Actions to do if collision with given interest-point
        private Dictionary<Rectangle, Action> interestPoints;
        // The distance from outer egdes of level that player can walk
        private Tuple<int, int> boundaries;
        private bool loaded;

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

        public Tuple<int, int> Boundaries
        {
            get
            {
                return new Tuple<int, int>(boundaries.Item1, Width - boundaries.Item2);
            }

            set
            {
                boundaries = value;
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

        public Vector2 SpawnPoint
        {
            get
            {
                return spawnPoint;
            }

            set
            {
                spawnPoint = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public Level(string imageString, Vector2 spawnPoint, Tuple<int, int> boundaries)
        {
            this.imageString = imageString;
            this.InterestPoints = new Dictionary<Rectangle, Action>();
            this.boundaries = boundaries;
            this.SpawnPoint = spawnPoint;
            this.name = imageString;
            this.loaded = false;
            LoadContent(GameWorld.Instance.Content);
        }

        public void LoadContent(ContentManager content)
        {
            if(!this.loaded)
            {
                background = content.Load<Texture2D>(imageString);            

                this.loaded = true;
            }
            else
            {
                // getting loaded twice means player requested load
                GameWorld.Instance.Player.GetTransform.Position = SpawnPoint;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle((int)GameWorld.Instance.Camera.Position.X, (int)GameWorld.Instance.Camera.Position.Y, GameWorld.Instance.DisplayRect.Width, GameWorld.Instance.DisplayRect.Height);

            spriteBatch.Draw(background, Vector2.Zero, sourceRect, Color.White);
        }
    }
}
