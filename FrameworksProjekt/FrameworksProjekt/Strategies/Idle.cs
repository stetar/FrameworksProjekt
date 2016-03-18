using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    class Idle : IStrategy
    {
        Animator animator;

        public Idle(Animator animator)
        {
            this.animator = animator;
        }

        public void Execute(ref Direction direction)
        {
            
        }
    }
}
