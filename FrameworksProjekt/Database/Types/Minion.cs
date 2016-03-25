using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt.Database.Types
{
    class Minion
    {
        public long ID { get; set; }
        public double Speed { get; set; }
        public double Strength { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public string currentLevelName { get; set; }
        public string targetLevelName { get; set; }
        public string imagePath { get; set; }
    }
}
