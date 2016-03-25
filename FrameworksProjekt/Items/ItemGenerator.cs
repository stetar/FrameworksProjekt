using FrameworksProjekt.Database.Factories;
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
        private ItemFac ifa = new ItemFac();

        public ItemGenerator()
        {
            initItemNames();
        }

        public void initItemNames()
        {
            itemNames = new string[5][];
                
            for(int i = 0; i < itemNames.Length; i++)
            {
                itemNames[i] = ifa.GetBy("Category", i.ToString()).Select(x => x.Name).ToArray();
            }
            //itemNames[0] = new string[]{ "Banana", "Apple", "Pear", "Cherrys", "Peaches", "Grapes" };
            //itemNames[1] = new string[] { "Cheeseburger", "Hotdog", "Roast", "Shawama", "Pizza" };
            //itemNames[2] = new string[] { "Arrmani pantalones", "Polka dotted underpants" };
            //itemNames[3] = new string[] { "Couch", "Chair", "Boring Desk" };
            //itemNames[4] = new string[] { "Aye-Pad", "Piratey pun" };
        }

        public Item GenerateItem(Category category, int maxCount)
        {
            // get random item name from array corresponding to categorys
            string name = itemNames[(int)category][r.Next(itemNames[(int)category].Length)];
            int count = r.Next(maxCount-1) + 1;

            return new Item(name, count, category);
        }
    }
}
