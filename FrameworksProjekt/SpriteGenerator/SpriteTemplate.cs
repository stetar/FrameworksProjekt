using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FrameworksProjekt.SpriteGenerator
{
    class SpriteTemplate
    {
        private Image sprite;
        private PointF offset;

        public PointF Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }

        public Image Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public SpriteTemplate(string imagePath, PointF offset)
        {
            this.sprite = Image.FromFile(imagePath);
            this.offset = offset;
        }
    }
}
