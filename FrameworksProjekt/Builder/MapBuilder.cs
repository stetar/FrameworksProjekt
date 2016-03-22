using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FrameworksProjekt.Interfaces;

namespace FrameworksProjekt.Builder
{
    class MapBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new Level("Denmark", new Vector2(-150, -150), new Tuple<int, int>(0, 0));
        }
    }

}
