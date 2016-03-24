using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt.Builder
{
    class CoffeeshopBuilder : ILBuilder
    {

        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new InsideLevel("Cellar", new Vector2(500, 500), new Tuple<int, int>(400, 100), new Vector2(1322, 550), new OutsideLevel("Aarhus", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Aarhus));
            l.InterestPoints.Add(new Rectangle(1222, 500, 100, 100), () => DirectoryAction());
        }

        public void DirectoryAction()
        {
            LevelDirector ld = new LevelDirector(new AarhusBuilder());
            l = ld.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }
    }
}
