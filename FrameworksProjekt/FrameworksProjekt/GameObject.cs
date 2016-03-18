using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace FrameworksProjekt
{
    public class GameObject
    {
        private Transform transform;
        private List<Component> components;

        public Transform GetTransform
        {
            get { return transform; }
        }

        public GameObject(Vector2 position)
        {
            components = new List<Component>();

            this.transform = new Transform(this, position);

            AddComponent(transform);
        }

        public void LoadContent(ContentManager content)
        {
            foreach(Component c in components)
            {
                if(c is ILoadable)
                {
                    (c as ILoadable).LoadContent(content);
                }
            }
        }

        public void Update()
        {
            foreach(Component c in components)
            {
                if(c is IUpdateable)
                {
                    (c as IUpdateable).Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Component c in components)
            {
                if(c is IDrawable)
                {
                    (c as IDrawable).Draw(spriteBatch);
                }
            }
        }

        public void AddComponent(Component component)
        {
            components.Add(component);
        }

        public Component GetComponent(string component)
        {
            Component toReturn = null;

            foreach(Component c in components)
            {
                if(c.GetType().Name == component)
                {
                    toReturn = c;
                }
            }

            return toReturn;
        }

        public void DeleteComponent(string component)
        {
            foreach(Component c in components.ToList())
            {
                if(c.GetType().Name == component)
                {
                    components.Remove(c);
                }
            }
        }
    }
}
