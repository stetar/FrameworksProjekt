using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FrameworksProjekt
{
    interface IDrawable
    {
        void Draw(SpriteBatch spriteBatch);
    }
}
