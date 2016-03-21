using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    class InsideLevel : Level
    {
        private Vector2 spawnPoint;
        private Vector2 exitPoint;

        public Vector2 ExitPoint
        {
            get
            {
                return exitPoint;
            }

            set
            {
                exitPoint = value;
            }
        }

        public InsideLevel(string imageString, Vector2 spawnPoint, Vector2 exitPoint, Tuple <int, int> boundaries) : base(imageString, boundaries)
        {
            this.spawnPoint = spawnPoint;
            this.ExitPoint = exitPoint;
            SetPlayerPosition();
        }

        public void SetPlayerPosition()
        {
            Vector2 spawn = spawnPoint;

            if(spawnPoint.X < Boundaries.Item1)
            {
                spawn.X = Boundaries.Item1;
            }
            else if(spawnPoint.X > Boundaries.Item2)
            {
                spawn.X = Boundaries.Item2;
            }

            GameWorld.Instance.Player.GetTransform.Position = spawnPoint;
        }

        public void CreateInterestPoints()
        { 

        }
    }
}
