using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;
using FrameworksProjekt.Components;

namespace FrameworksProjekt.Builder
{
    class AarhusBuilder : ILBuilder
    {
        private Level l;
        private List<LootItem> items;

        public List<LootItem> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
            }
        }

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new Level("Aarhus", new Tuple<int, int>(100,100));
            l.InterestPoints.Add(new Rectangle(500, 500, 100, 100), () => DirectoryAction());
        }

        public void DirectoryAction()
        {
            LevelDirector LD = new LevelDirector(new CellarBuilder());
            l = LD.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }
    }
}
