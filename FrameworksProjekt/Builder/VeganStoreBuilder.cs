using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt.Builder
{
    class VeganStoreBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new InsideLevel("Vegan store", new Vector2(0, 500), new Tuple<int, int>(0, 0), new Vector2(50, 550), new OutsideLevel("København", new Vector2(0, 500), new Tuple<int, int>(0, 0), City.København));
        }

        public void DirectoryAction()
        {
            //Add functionality here.
        }
    }
}
