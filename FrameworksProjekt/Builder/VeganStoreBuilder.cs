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
            l = new Level("Vegan store", new Vector2(0, 500), new Tuple<int, int>(0, 0));
        }

        public void DirectoryAction()
        {

        }
    }
}
