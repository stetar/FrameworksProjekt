using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;

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
            l = new Level("Skagen", new Tuple<int, int>(100, 100));
        }

        public void DirectoryAction()
        {
            //Add functionality 
        }
    }
}
