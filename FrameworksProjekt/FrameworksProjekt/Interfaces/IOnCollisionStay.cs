using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;

namespace FrameworksProjekt.Interfaces
{
    interface IOnCollisionStay
    {
        void OnCollisonStay(Collider other);
    }
}
