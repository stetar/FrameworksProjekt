using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt.Builder
{
    class MinionBuilder : IBuilder
    {
        private GameObject go;

        public GameObject GetResult()
        {
            return go;
        }

        public void BuildGameObject()
        {
            go = new GameObject(Vector2.Zero);
            go.AddComponent(new SpriteRenderer(go, "pirate1Spritesheet", 1, Color.White));
            go.AddComponent(new Minion(go));
            go.AddComponent(new Animator(go));
        }
    }
}
