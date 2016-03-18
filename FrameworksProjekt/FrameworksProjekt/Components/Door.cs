using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;

namespace FrameworksProjekt.Components
{
    class Door : Component
    {
        private InterestPoint interestPoint;

        public InterestPoint InterestPoint
        {
            get { return interestPoint; }
            set { interestPoint = value; }
        }

        public Door(GameObject gameObject) : base(gameObject)
        {
            
        }

        public void 
    }
}
