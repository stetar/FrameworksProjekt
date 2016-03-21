using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt.Builder
{
    class CellarBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new Level("Cellar", new Tuple<int, int>(320,100));
            l.InterestPoints.Add(new Rectangle(500, 500, 100, 100), () => DirectoryAction());
        }

        public void DirectoryAction()
        {

        }
    }
}
