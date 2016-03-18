using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    static class LevelBuilder
    {

        public static void BuildLevel1()
        {

        }

        public static void BuildCellarLvl()
        {
            Level l = new InsideLevel("Cellar");
            GameWorld.Instance.LoadLevel(l);

            GameWorld.Instance.GameLevel = l;
        }
    }
}
