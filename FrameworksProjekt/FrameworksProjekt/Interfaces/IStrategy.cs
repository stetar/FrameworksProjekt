using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    interface IStrategy
    {
        void Execute(ref Direction direction);
    }
}
