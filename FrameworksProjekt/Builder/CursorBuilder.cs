using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;
using Microsoft.Xna.Framework;

namespace FrameworksProjekt.Builder
{
    class CursorBuilder : IGOBuilder
    {
        private GameObject go;

        public GameObject GetResult()
        {
            return go;
        }

        public void BuildGameObject()
        {
            go = new GameObject(new Vector2(0, 0));
            go.AddComponent(new SpriteRenderer(go, "Hook", 1, Color.White));
            go.AddComponent(new Collider(go));
            go.AddComponent(new Cursor(go));

            go.LoadContent(GameWorld.Instance.Content);
        }
    }
}
