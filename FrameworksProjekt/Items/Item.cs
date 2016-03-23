using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace FrameworksProjekt.Items
{
    public enum Category
    {
        Fruit,
        Food,
        Clothes,
        Furniture,
        Electronics
    }

    public class Item
    {
        private int count;
        private string name;
        private static string[] imagePaths = {"Apple", "CheeseBurger", "Cloths", "Sofa", "Electronics" };
        private Category category;
        private Texture2D sprite;

        public int Count
        {
            get
            {
                return count;
            }

            set
            {
                count = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        internal Category Category
        {
            get
            {
                return category;
            }

            set
            {
                category = value;
            }
        }

        public Texture2D Sprite
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

        public Item(string name, int count, Category category)
        {
            this.Count = count;
            this.Name = name;
            this.Category = category;

            LoadContent();
        }

        public void LoadContent()
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(imagePaths[(int)category]);
        }
    }
}
