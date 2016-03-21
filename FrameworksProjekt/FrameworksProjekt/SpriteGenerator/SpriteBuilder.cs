using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrameworksProjekt
{
    static class SpriteBuilder
    {
        private static List<SpriteFrame> frames;

        public static List<SpriteFrame> Frames
        {
            get
            {
                if(frames == null)
                {
                    initFrames();
                }

                return frames;
            }
        }

        public static string GetSprite()
        {
            return SpriteGenerator.GenerateSprite(Frames);
        }

        public static void initFrames()
        {
            frames = new List<SpriteFrame>();

            // First frame list of components needs to have all components - any other frame component list can exclude an item by replacing it with any string
            frames.Add(new SpriteFrame("Frame1", false, new string[] { "Outfits", "Faces", "Hats", "Shoes", "Arms" }, new PointF[] { new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0) }, 0, 0));
            frames.Add(new SpriteFrame("Frame2", false, new string[] { "Outfits", "Faces", "Hats", "Shoes", "Arms" }, new PointF[] { new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0) }, 128, 0));
            frames.Add(new SpriteFrame("Frame3", false, new string[] { "Outfits", "Faces", "Hats", "Shoes", "Arms" }, new PointF[] { new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0) }, 0, 128));
            frames.Add(new SpriteFrame("Frame4", false, new string[] { "Outfits", "Faces", "Hats", "Shoes", "Arms" }, new PointF[] { new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0) }, 128, 128));
            frames.Add(new SpriteFrame("Frame3", true, new string[] { "Outfits", "Faces", "Hats", "Shoes", "Arms" }, new PointF[] { new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0) }, 0, 256));
            frames.Add(new SpriteFrame("Frame4", true, new string[] { "Outfits", "Faces", "Hats", "Shoes", "Arms" }, new PointF[] { new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0), new PointF(0, 0) }, 128, 256));
        }
    }
}
