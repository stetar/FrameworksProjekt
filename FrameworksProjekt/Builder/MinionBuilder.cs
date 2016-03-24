using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;
using Microsoft.Xna.Framework;
using FrameworksProjekt.SpriteGenerator;

namespace FrameworksProjekt.Builder
{
    class MinionBuilder : IGOBuilder
    {
        private GameObject go;

        public GameObject GetResult()
        {
            return go;
        }

        public void BuildGameObject()
        {
            go = new GameObject(Vector2.Zero);
            go.AddComponent(new SpriteRenderer(go, "Pirates/"+SpriteBuilder.GetSprite(), 1, Color.White));
            go.AddComponent(new Minion(go));
            go.AddComponent(new Animator(go));
            go.AddComponent(new Collider(go));
        }
    }
}
