using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt.Builder
{
    class SkagenBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new OutsideLevel("Skagen", new Vector2(0, 500), new Tuple<int, int>(0, 0), City.Skagen);
        }

        public void DirectoryAction()
        {

        }
    }
}
