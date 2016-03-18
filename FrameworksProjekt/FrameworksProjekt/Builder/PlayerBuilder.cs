using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt
{
    class PlayerBuilder : IBuilder
    {
        private GameObject go;

        public GameObject GetResult()
        {
            return go;
        }

        public void BuildGameObject()
        {
            go = new GameObject(new Vector2(0, 500));
            go.AddComponent(new SpriteRenderer(go, "pirate1Spritesheet", 1, Color.White));
            go.AddComponent(new Player(go));
            go.AddComponent(new Animator(go));
        }
    }
}
