﻿using System;
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
            l = new OutsideLevel("Aarhus", new Vector2(120, 500), new Tuple<int, int>(-120, -120), City.Aarhus, new Vector2(50, 500), new Vector2(1660, 550));
            l.InterestPoints.Add(new Rectangle(40, 400, 80, 200), () => MapAction());
            l.InterestPoints.Add(new Rectangle(1640, 400, 50, 200), () => ShopAction());
            l.InterestPoints.Add(new Rectangle(2540, 400, 100, 200), () => CoffeeShop());

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

        public void ShopAction()
        {
            ((OutsideLevel)l).ShopAction();
        }

        public void CoffeeShop()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                LevelDirector ld = new LevelDirector(new CoffeeshopBuilder());
                l = ld.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(2520, 350, 180, 40), "Coffee Shop.",
                new Vector2(10, 10), Color.LightGray, Color.Black));
            }
        }
    }
}
