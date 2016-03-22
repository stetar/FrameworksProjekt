using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;

namespace FrameworksProjekt.Builder
{
    class LevelDirector
    {
        private ILBuilder builder;

        public LevelDirector(ILBuilder builder)
        {
            this.builder = builder;
        }

        public Level Construct()
        {
            builder.BuildLevel();

            return builder.GetResult();
        }
    }
}
