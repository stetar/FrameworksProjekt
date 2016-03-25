using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt.Database.Types
{
    class Inventory
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public long Item { get; set; }
        public long Count { get; set; }
    }
}
