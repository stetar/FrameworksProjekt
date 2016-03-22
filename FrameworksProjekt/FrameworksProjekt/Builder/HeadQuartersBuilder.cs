﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;

namespace FrameworksProjekt.Builder
{
    class HeadQuartersBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new Level("Headquarters", new Tuple<int, int>(400,100));
        }

        public void DirectoryAction()
        {
               
        }
    }
}