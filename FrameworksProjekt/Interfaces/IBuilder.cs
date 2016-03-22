using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    interface IBuilder
    {
        GameObject GetResult();

        void BuildGameObject();
    }
}
