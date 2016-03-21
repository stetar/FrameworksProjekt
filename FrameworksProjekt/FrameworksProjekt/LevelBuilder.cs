using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt
{
    static class LevelBuilder
    {
        private static bool raided;

        public static void BuildLevel1()
        {

        }

        public static void BuildCellarLvl()
        {
            Level l = new InsideLevel("Cellar", new Vector2(0,0), new Vector2(0,0) );
            GameWorld.Instance.LoadLevel(l);

            GameWorld.Instance.GameLevel = l;

            l.InterestPoints.Add(new Rectangle(500, 500, 150, 150),
                () => InterestPointCellar());
        }

        public static void InterestPointCellar()
        {
            if (!raided)
            {
                foreach (GameObject obj in GameWorld.Instance.GameObjects)
                {
                    obj.YPhone++;
                }
                raided = true;
            }
        }
    }
}
