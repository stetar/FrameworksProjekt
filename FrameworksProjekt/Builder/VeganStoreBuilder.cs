﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt.Builder
{
    class VeganStoreBuilder : ILBuilder
    {
        private Level l;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new InsideLevel("Vegan store", new Vector2(0, 500), new Tuple<int, int>(400, 100), new Vector2(50, 550), new OutsideLevel("København", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.København, new Vector2(50, 500), new Vector2(1605, 550)));
            l.InterestPoints.Add(new Rectangle(1222, 500, 100, 100), () => DirectoryAction());
        }

        public void DirectoryAction()
        {
            LevelDirector ld = new LevelDirector(new KøbenhavnBuilder());
            l = ld.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }
    }
}
