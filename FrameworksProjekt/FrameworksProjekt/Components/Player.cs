using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FrameworksProjekt
{
    enum Direction
    {
        Down, 
        Left, 
        Right
    }

    class Player : Component, ILoadable, IUpdateable
    {
        private float speed = 100;
        KeyboardState ks;
        public Animator animator;
        private Direction direction;
        //private IStrategy strategy;

        public Player(GameObject gameObject) : base(gameObject)
        {
            direction = Direction.Down;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
        }

        public void LoadContent(ContentManager content)
        {
            
        }

        public void Update()
        {
            
        }
    }
}
