using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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
            l = new OutsideLevel("Grenaa", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Grenaa, new Vector2(50, 500), new Vector2(1650, 550));
            l.InterestPoints.Add(new Rectangle(1630, 400, 100, 200), () => ShopAction());
            l.InterestPoints.Add(new Rectangle(300, 400, 100, 200), () => DirectoryAction());
            l.InterestPoints.Add(new Rectangle(40, 400, 80, 200), () => MapAction());
            l.InterestPoints.Add(new Rectangle(2060, 400, 100, 200), () => Cellar());
        }

        public void DirectoryAction()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                LevelDirector LD = new LevelDirector(new HeadQuartersBuilder());
                l = LD.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(250, 250, 260, 40), "Click Space to enter headquarters",
                   new Vector2(10, 10), Color.LightGray, Color.Black));
            }
        }

        public void ShopAction()
        {
            ((OutsideLevel)l).ShopAction();
        }

        public void MapAction()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                LevelDirector LD = new LevelDirector(new WorldMapBuilder());
                l = LD.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(30, 350, 100, 40), "World map.",
                   new Vector2(10, 10), Color.LightGray, Color.Black));
            }
        }

        public void Cellar()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                LevelDirector ld = new LevelDirector(new CellarBuilder());
                l = ld.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(2020, 350, 180, 40), "Gaming klubben.",
                new Vector2(10, 10), Color.LightGray, Color.Black));
            }
        }
    }
}
