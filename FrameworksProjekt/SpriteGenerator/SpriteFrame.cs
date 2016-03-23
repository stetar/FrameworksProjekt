using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FrameworksProjekt.SpriteGenerator
{
    class SpriteFrame
    {
        private bool reverse;
        private string folderName;
        private PointF position;
        private string[] components;
        private PointF[] offsets;


        public bool Reverse
        {
            get
            {
                return reverse;
            }

            set
            {
                reverse = value;
            }
        }

        public string FolderName
        {
            get
            {
                return folderName;
            }

            set
            {
                folderName = value;
            }
        }

        public string[] Components
        {
            get
            {
                return components;
            }

            set
            {
                components = value;
            }
        }

        public PointF[] Offsets
        {
            get
            {
                return offsets;
            }

            set
            {
                offsets = value;
            }
        }

        public PointF Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public SpriteFrame(string folderName, bool reverse, string[] components, PointF[] offsets, int targetX, int targetY)
        {
            this.folderName = folderName;
            this.reverse = reverse;
            this.components = components;
            this.offsets = offsets;
            this.position = new PointF(targetX, targetY);
        }
    }
}
