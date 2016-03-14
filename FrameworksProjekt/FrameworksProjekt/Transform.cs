using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    public class Transform : Component
    {
        public Vector2 Position { get; set; }

        public Transform(GameObject gameObject, Vector2 position) : base(gameObject)
        {
            this.Position = position;
        }

        public void Translate(Vector2 translation)
        {
            this.Position += translation;
        }
    }
}
