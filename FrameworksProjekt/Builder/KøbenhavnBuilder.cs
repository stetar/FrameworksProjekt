using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;

namespace FrameworksProjekt.Builder
{
    class KøbenhavnBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new Level("København");
        }

        public void DirectoryAction()
        {

        }
    }
}
