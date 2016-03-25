﻿using FrameworksProjekt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FrameworksProjekt.Builder
{
    class LootMapBuilder : ILBuilder
    {

        private Level l;
        private Cursor cursor;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new Level("Denmark2", new Vector2(-150, -150), new Tuple<int, int>(0, 0));
            l.InterestPoints.Add(new Rectangle(611, 172, 75, 55), () => SendToSkagen());
            l.InterestPoints.Add(new Rectangle(688, 364, 55, 40), () => SendToGrenaa());
            l.InterestPoints.Add(new Rectangle(580, 405, 75, 55), () => SendToAarhus());
            l.InterestPoints.Add(new Rectangle(359, 472, 75, 55), () => SendToEsbjerg());
            l.InterestPoints.Add(new Rectangle(885, 519, 75, 55), () => SendToKøbenhavn());

            GameObjectDirector gd = new GameObjectDirector(new CursorBuilder());
            GameObject g = gd.Construct();
            cursor = ((Cursor)g.GetComponent("Cursor"));
            GameWorld.Instance.GameObjects.Add(g);
        }

        public void SendToGrenaa()
        {

            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new GrenaaBuilder());
                l = LD.Construct();
                GameWorld.Instance.ActiveMinion.TargetLevel = l;
                GoToHeadQuarter();
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(630, 300, 250, 60), "Click to plunder Grenaa",
                    new Vector2(10, 10), Color.White, Color.Black));
            }

        }

        public void SendToAarhus()
        {
            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new AarhusBuilder());
                l = LD.Construct();
                GameWorld.Instance.ActiveMinion.TargetLevel = l;
                GoToHeadQuarter();
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(520, 360, 170, 40), "Click to plunder Aarhus",
                    new Vector2(10, 10), Color.White, Color.Black));
            }
        }

        public void SendToSkagen()
        {

            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new SkagenBuilder());
                l = LD.Construct();
                GameWorld.Instance.ActiveMinion.TargetLevel = l;
                GoToHeadQuarter();
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(570, 130, 170, 40), "Click to plunder Skagen",
                    new Vector2(10, 10), Color.White, Color.Black));
            }
        }


        public void SendToEsbjerg()
        {
            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new EsbjergBuilder());
                l = LD.Construct();
                GameWorld.Instance.ActiveMinion.TargetLevel = l;
                GoToHeadQuarter();
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(320, 430, 170, 40), "Click to plunder Esbjerg",
                    new Vector2(10, 10), Color.White, Color.Black));
            }
        }

        public void SendToKøbenhavn()
        {
            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new KøbenhavnBuilder());
                l = LD.Construct();
                GameWorld.Instance.ActiveMinion.TargetLevel = l;
                GoToHeadQuarter();
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(850, 480, 200, 40), "Click to plunder København",
                    new Vector2(10, 10), Color.White, Color.Black));
            }
        }

        public void GoToHeadQuarter()
        {
            LevelDirector LD = new LevelDirector(new HeadQuartersBuilder());
            l = LD.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }
    }
}
