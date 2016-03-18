using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrameworksProjekt.Components
{
    public class Collider : Component, IDrawable, ILoadable, IUpdateable
    {
        private SpriteRenderer spriteRenderer;
        private Texture2D sprite;
        private bool doCollisionCheck;

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle
                    (
                        (int)(GameObject.GetTransform.Position.X + spriteRenderer.Offset.X),
                        (int)(GameObject.GetTransform.Position.Y + spriteRenderer.Offset.Y),
                        spriteRenderer.Rectangle.Width,
                        spriteRenderer.Rectangle.Height
                    );
            }
        }

        public bool DoCollisionCheck
        {
            get { return doCollisionCheck; }
            set { doCollisionCheck = value; }
        }

        public Collider(GameObject gameObject) : base(gameObject)
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            spriteRenderer = (SpriteRenderer) GameObject.GetComponent("SpriteRenderer");

            sprite = content.Load<Texture2D>("CollisionTexture");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
#if DEBUG
            Rectangle topLine = new Rectangle(CollisionBox.X, CollisionBox.Y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X, CollisionBox.Y + CollisionBox.Height, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width, CollisionBox.Y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X, CollisionBox.Y, 1, CollisionBox.Height);

            spriteBatch.Draw(sprite, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(sprite, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(sprite, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(sprite, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
#endif
            
        }

        public void CheckCollision()
        {
            if (doCollisionCheck)
            {
                foreach (Collider other in GameWorld.Instance.Colliders)
                {
                    if (other != this)
                    {
                        if (CollisionBox.Intersects(other.CollisionBox))
                        {
                            GameObject.OnCollisionStay(other);
                        }
                    }
                }
            }
        }

        public void Update()
        {
            CheckCollision();
        }
    }
}
