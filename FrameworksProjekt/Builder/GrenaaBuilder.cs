using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt.Builder
{
    class GrenaaBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new OutsideLevel("Grenaa", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Grenaa);

        }

        public void DirectoryAction()
        {
            LevelDirector LD = new LevelDirector(new HeadQuartersBuilder());
            l = LD.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }

    }
}
