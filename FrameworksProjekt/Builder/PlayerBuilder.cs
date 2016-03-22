using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt
{
    class PlayerBuilder : IGOBuilder
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
            go.AddComponent(new Collider(go));
        }
    }
}
