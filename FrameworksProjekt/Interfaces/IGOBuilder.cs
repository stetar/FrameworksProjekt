using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    interface IGOBuilder
    {
        GameObject GetResult();

        void BuildGameObject();
    }
}
