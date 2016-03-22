using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt.Items
{
    class ItemGenerator
    {

        public string[][] itemNames;
        private static Random r = new Random();

        public ItemGenerator()
        {
            initItemNames();
        }

        public void initItemNames()
        {
            itemNames = new string[5][];

            itemNames[0] = new string[]{ "Banana", "Apple", "Pear", "Cherrys", "Peaches", "Grapes" };
            itemNames[1] = new string[] { "Cheeseburger", "Hotdog", "Roast", "Shawama", "Pizza" };
            itemNames[2] = new string[] { "Arrmani pantalones", "Polka dotted underpants" };
            itemNames[3] = new string[] { "Couch", "Chair", "Boring Desk" };
            itemNames[4] = new string[] { "Aye-Pad", "Piratey pun" };
        }

        public Item GenerateItem(Category category, int maxCount)
        {
            string name = itemNames[(int)category][r.Next(itemNames[(int)category].Length)];
            int count = r.Next(maxCount-1) + 1;

            return new Item(name, count, category);
        }
    }
}
