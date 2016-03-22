using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Components;

namespace FrameworksProjekt.Builder
{
    class LootItemBuilder
    {
        private string imageString;
        private Vector2 position;
        private GameObject go;

        public LootItemBuilder(Vector2 position, string imagePath)
        {
            this.position = position;
            this.imageString = imagePath;
        }

        public GameObject GetResult()
        {
            return go;
        }

        public void BuildGameObject()
        {
            go = new GameObject(position);
            go.AddComponent(new SpriteRenderer(go, imageString, 1, Color.White));
            go.AddComponent(new LootItem(go));
            go.AddComponent(new Collider(go));
        }
    }
}
