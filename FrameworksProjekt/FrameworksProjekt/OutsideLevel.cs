﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt
{
    class OutsideLevel : Level
    {

        public OutsideLevel(string imageString) : base(imageString)
        {

        }

        public override void CreateInterestPoints()
        {
            GameWorld.Instance.GameLevel.InterestPoints.Add(new Rectangle(), );
        }
    }
}
