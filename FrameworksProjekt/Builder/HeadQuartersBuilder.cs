using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;

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
            l = new Level("Headquarters", new Vector2(350, 500), new Tuple<int, int>(240, -100));
            l.InterestPoints.Add(new Rectangle(240, 400, 10, 200), () => DirectoryAction());
            l.InterestPoints.Add(new Rectangle(800, 300, 400, 400), () => Inventory());
            l.InterestPoints.Add(new Rectangle(1400, 300, 100, 400), () => DirectoryAction2());
        }

        public void DirectoryAction()
        {
            LevelDirector ld = new LevelDirector(new AarhusBuilder());
            l = ld.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }

        public void Inventory()
        {
            GameWorld.Instance.DrawInventory = true;
        }

        public void DirectoryAction2()
        {
            LevelDirector ld = new LevelDirector(new CellarBuilder());
            l = ld.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }
    }
}
