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

        public InsideLevel(string imageString, Vector2 spawnPoint, Vector2 exitPoint) : base(imageString)
        {
            this.spawnPoint = spawnPoint;
            this.exitPoint = exitPoint;
        }

        public void CreateInterestPoints()
        { 

        }
    }
}
