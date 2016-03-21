using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    class OutsideLevel : Level
    {

        public OutsideLevel(string imageString, Tuple<int, int> boundaries) : base(imageString, boundaries)
        {

        }

        public override void CreateInterestPoints()
        {

        }
    }
}
