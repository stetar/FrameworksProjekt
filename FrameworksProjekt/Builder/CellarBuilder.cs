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
            l = new InsideLevel("Cellar", new Vector2(600, 500), new Tuple<int, int>(350, 100), new Vector2(1322, 550), new OutsideLevel("Grenaa", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Grenaa));
            l.InterestPoints.Add(new Rectangle(1222, 500, 100, 100), () => DirectoryAction());
        }

        public void DirectoryAction()
        {
            LevelDirector ld = new LevelDirector(new GrenaaBuilder());
            l = ld.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }
    }
}
