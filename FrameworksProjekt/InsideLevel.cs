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

        public InsideLevel(string imageString, Vector2 spawnPoint, Tuple<int, int> boundaries) : base(imageString, spawnPoint, boundaries)
        {

        }

        public void CreateInterestPoints()
        { 

        }
    }
}
