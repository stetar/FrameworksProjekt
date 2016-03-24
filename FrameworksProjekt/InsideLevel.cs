using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    class InsideLevel : Level
    {
        private Vector2 minionExitPoint;
        private OutsideLevel city;

        public Vector2 MinionExitPoint
        {
            get
            {
                return minionExitPoint;
            }

            set
            {
                minionExitPoint = value;
            }
        }

        public OutsideLevel City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public InsideLevel(string imageString, Vector2 spawnPoint, Tuple<int, int> boundaries, Vector2 minionExitPoint, OutsideLevel city) : base(imageString, spawnPoint, boundaries)
        {
            this.minionExitPoint = minionExitPoint;
            this.city = city;
        }
    }
}
