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

        public InsideLevel(string imageString, Vector2 spawnPoint, Vector2 exitPoint) : base(imageString)
        {
            this.spawnPoint = spawnPoint;
            this.ExitPoint = exitPoint;
            ((Transform)GameWorld.Instance.Player.GetComponent("Transform")).Position = spawnPoint;
        }

        public override void CreateInterestPoints()
        {
            
        }
    }
}
