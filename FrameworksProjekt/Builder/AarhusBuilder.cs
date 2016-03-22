using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Interfaces;
using Microsoft.Xna.Framework;

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
            l = new Level("Aarhus");
            l.InterestPoints.Add(new Rectangle(500, 500, 100, 100), () => DirectoryAction());
        }

        public void DirectoryAction()
        {
            LevelDirector LD = new LevelDirector(new CellarBuilder());
            l = LD.Construct();
            GameWorld.Instance.GameLevel = l;
            GameWorld.Instance.LoadLevel(l);
        }
    }
}
