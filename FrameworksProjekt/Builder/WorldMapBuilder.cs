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
        private MouseState mouseState = Mouse.GetState();

        public Level GetResult()
        {
            return l;
        }

        public void BuildLevel()
        {
            l = new Level("Denmark", new Vector2(-150,-150), new Tuple<int, int>(0,0) );
            l.InterestPoints.Add(new Rectangle(611, 172, 75, 55), () => GoToSkagen());
            l.InterestPoints.Add(new Rectangle(688, 364, 55, 40), () => GoToGrenaa());
            l.InterestPoints.Add(new Rectangle(580, 405, 75, 55), () => GoToAarhus());
            l.InterestPoints.Add(new Rectangle(359, 472, 75, 55), () => GoToEsbjerg());
            l.InterestPoints.Add(new Rectangle(885, 519, 75, 55), () => GoToKøbenhavn());
        }

        public void GoToGrenaa()
        {
            foreach (Rectangle key in GameWorld.Instance.GameLevel.InterestPoints.Keys)
            {
                if (key.Intersects(cursor.Rectangle))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        LevelDirector LD = new LevelDirector(new GrenaaBuilder());
                        l = LD.Construct();
                        GameWorld.Instance.GameLevel = l;
                        GameWorld.Instance.LoadLevel(l);
                    }
                }
            }
        }

        public void GoToAarhus()
        {
            foreach (Rectangle key in GameWorld.Instance.GameLevel.InterestPoints.Keys)
            {
                if (key.Intersects(cursor.Rectangle))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        LevelDirector LD = new LevelDirector(new AarhusBuilder());
                        l = LD.Construct();
                        GameWorld.Instance.GameLevel = l;
                        GameWorld.Instance.LoadLevel(l);
                    }
                }
            }
        }

        public void GoToSkagen()
        {
            foreach (Rectangle key in GameWorld.Instance.GameLevel.InterestPoints.Keys)
            {
                if (key.Intersects(cursor.Rectangle))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        LevelDirector LD = new LevelDirector(new SkagenBuilder());
                        l = LD.Construct();
                        GameWorld.Instance.GameLevel = l;
                        GameWorld.Instance.LoadLevel(l);
                    }
                }
            }
        }

        public void GoToEsbjerg()
        {
            foreach (Rectangle key in GameWorld.Instance.GameLevel.InterestPoints.Keys)
            {
                if (key.Intersects(cursor.Rectangle))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        LevelDirector LD = new LevelDirector(new EsbjergBuilder());
                        l = LD.Construct();
                        GameWorld.Instance.GameLevel = l;
                        GameWorld.Instance.LoadLevel(l);
                    }
                }
            }
        }

        public void GoToKøbenhavn()
        {
            foreach (Rectangle key in GameWorld.Instance.GameLevel.InterestPoints.Keys)
            {
                if (key.Intersects(cursor.Rectangle))
                {
                    if (mouseState.LeftButton == ButtonState.Pressed)
                    {
                        LevelDirector LD = new LevelDirector(new KøbenhavnBuilder());
                        l = LD.Construct();
                        GameWorld.Instance.GameLevel = l;
                        GameWorld.Instance.LoadLevel(l);
                    }
                }
            }
        }
    }
}
