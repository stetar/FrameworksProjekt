using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace FrameworksProjekt.Builder
{
    class WorldMapBuilder : ILBuilder
    {
        private Level l;
        private Cursor cursor;

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new Level("Denmark", new Vector2(-150, -150), new Tuple<int, int>(0, 0));
            l.InterestPoints.Add(new Rectangle(611, 172, 75, 55), () => GoToSkagen());
            l.InterestPoints.Add(new Rectangle(688, 364, 55, 40), () => GoToGrenaa());
            l.InterestPoints.Add(new Rectangle(580, 405, 75, 55), () => GoToAarhus());
            l.InterestPoints.Add(new Rectangle(359, 472, 75, 55), () => GoToEsbjerg());
            l.InterestPoints.Add(new Rectangle(885, 519, 75, 55), () => GoToKøbenhavn());

            GameObjectDirector d = new GameObjectDirector(new CursorBuilder());
            GameObject g = d.Construct();
            cursor = ((Cursor)g.GetComponent("Cursor"));
            GameWorld.Instance.GameObjects.Add(g);
        }

        public void GoToGrenaa()
        {

            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new GrenaaBuilder());
                l = LD.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(630, 300, 250, 60), "Click to go to Grenaa\nthe Execution Dock of your gang",
                    new Vector2(10, 10), Color.White, Color.Black));
            }

        }

        public void GoToAarhus()
        {
            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new AarhusBuilder());
                l = LD.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(520, 360, 170, 40), "Click to go to Aarhus",
                    new Vector2(10, 10), Color.White, Color.Black));
            }
        }

        public void GoToSkagen()
        {

            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new SkagenBuilder());
                l = LD.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(570, 130, 170, 40), "Click to go to Skagen",
                    new Vector2(10, 10), Color.White, Color.Black));
            }
        }


        public void GoToEsbjerg()
        {
            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new EsbjergBuilder());
                l = LD.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(320, 430, 170, 40), "Click to go to Esbjerg",
                    new Vector2(10, 10), Color.White, Color.Black));
            }
        }

        public void GoToKøbenhavn()
        {
            if (cursor.State.LeftButton == ButtonState.Pressed)
            {
                LevelDirector LD = new LevelDirector(new KøbenhavnBuilder());
                l = LD.Construct();
                GameWorld.Instance.GameLevel = l;
                GameWorld.Instance.LoadLevel(l);
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(new Tooltip(new Rectangle(850, 480, 200, 40), "Click to go to København",
                    new Vector2(10, 10), Color.White, Color.Black));
            }
        }
    }
}
