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
        private bool doCollisionCheck = true;

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
            int x = (int)GameWorld.Instance.Camera.Position.X;
            int y = (int)GameWorld.Instance.Camera.Position.Y;

            Rectangle topLine = new Rectangle(CollisionBox.X - x, CollisionBox.Y - y, CollisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(CollisionBox.X - x, CollisionBox.Y + CollisionBox.Height - y, CollisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(CollisionBox.X + CollisionBox.Width - x, CollisionBox.Y - y, 1, CollisionBox.Height);
            Rectangle leftLine = new Rectangle(CollisionBox.X - x, CollisionBox.Y - y, 1, CollisionBox.Height);

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
                            doCollisionCheck = false;
                            GameObject.OnCollisionStay(other);
                        }
                    }
                }

                foreach (Rectangle key in GameWorld.Instance.GameLevel.InterestPoints.Keys)
                {
                    if (key.Intersects(this.CollisionBox))
                    {
                        GameWorld.Instance.GameLevel.InterestPoints[key]();
                    }
                }
            }


            doCollisionCheck = true;
        }

        public void Update()
        {
            CheckCollision();
        }
    }
}
