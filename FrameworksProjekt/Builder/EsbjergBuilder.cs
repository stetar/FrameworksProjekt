using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt.Builder
{
    class EsbjergBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new OutsideLevel("Esbjerg", new Vector2(0, 500), new Tuple<int, int>(0, 0), City.Esbjerg, new Vector2(100, 500));
        }

        public void DirectoryAction()
        {
            //Add functionality here.
        }
    }
}
