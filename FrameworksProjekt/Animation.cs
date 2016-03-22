using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    public class Animation
    {
        public int Frames { get; set; } 
        public float Fps { get; set; }
        public Vector2 Offset { get; set; }
        public Rectangle[] Rectangles { get; set; }

        public Animation(int frames, int yPos, int startXFrame, int width, int height, float fps, Vector2 offset)
        {
            Rectangles = new Rectangle[frames];

            Frames = frames;

            this.Offset = offset;

            this.Fps = fps;

            for(int i = 0; i < frames; i++)
            {
                Rectangles[i] = new Rectangle((i + startXFrame) * width, yPos, width, height);
            }
        }
    }
}
