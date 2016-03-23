using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FrameworksProjekt.Items;

namespace FrameworksProjekt.Builder
{
    class AarhusBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new OutsideLevel("Aarhus", new Vector2(20,500), new Tuple<int, int>(-120, -120), City.Aarhus);
            l.InterestPoints.Add(new Rectangle(-120, 400, 20, 200), () => DirectoryAction());
            l.InterestPoints.Add(new Rectangle(1640, 400, 50, 200), () => ShopAction());
            l.InterestPoints.Add(new Rectangle(4100, 400, 20, 200), () => DirectoryAction2());
        }

        public void DirectoryAction()
        {
            LevelDirector LD = new LevelDirector(new HeadQuartersBuilder());
            l = LD.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }


        public void DirectoryAction2()
        {
            LevelDirector ld = new LevelDirector(new WorldMapBuilder());
            l = ld.Construct();

            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }

        public void ShopAction()
        {
            ((OutsideLevel)l).ShopAction();
        }
    }
}
