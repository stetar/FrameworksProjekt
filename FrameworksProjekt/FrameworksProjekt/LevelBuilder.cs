using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt
{
    static class LevelBuilder
    {

        public static void BuildLevel1()
        {

        }

        public static void BuildCellarLvl()
        {
            Level l = new InsideLevel("Cellar", new Vector2(100, 500),  new Vector2(0, 500));
            GameWorld.Instance.LoadLevel(l);

            GameWorld.Instance.GameLevel = l;
        }
    }
}
