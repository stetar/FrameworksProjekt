using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworksProjekt.Items;

namespace FrameworksProjekt
{
    public class Inventory
    {
        private List<Item> items;

        internal List<Item> Items
        {
            get
            {
                return items;
            }

            set
            {
                items = value;
            }
        }

        public Inventory()
        {
            this.Items = new List<Item>();
        }

        public void AddItem(Item i)
        {
            Item it = GetItem(i.Name);

            if(it != null)
            {
                it.Count += i.Count;
            }
            else
            {
                Items.Add(i);
            }
        }

        public void RemoveItem(string itemName, int count)
        {
            Item it = GetItem(itemName);

            if(it != null)
            {
                if(it.Count - count > 0)
                {
                    it.Count -= count;
                }
                else
                {
                    Items.Remove(it);
                }
            }
        }

        public Item GetItem(string itemName)
        {
            return Items.Find(x => x.Name == itemName);
        }

        public bool CheckForItems(Item items)
        {
            Item it = GetItem(items.Name);
            bool toReturn = false;

            if (it != null && it.Count >= items.Count)
            {
                toReturn = true;
            }

            return toReturn;
        }
    }
}
