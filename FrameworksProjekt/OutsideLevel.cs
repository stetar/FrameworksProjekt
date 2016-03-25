using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FrameworksProjekt.Items;
using FrameworksProjekt.Components;

namespace FrameworksProjekt
{
    class OutsideLevel : Level
    {
        private Inventory inventory;
        private KeyboardState ks;
        private static Random r = new Random();
        Tooltip t = new Tooltip(new Rectangle(1520, 100, 300, 100), "Hold space to loot the store!", new Vector2(20, 20), Color.LightGray, Color.Black);
        Tooltip t1 = new Tooltip(new Rectangle(1520, 100, 300, 100), "Closed until further notice due to piracy.", new Vector2(10, 20), Color.LightGray, Color.Black);
        Tooltip t2 = new Tooltip(new Rectangle(1520, 100, 300, 100), "This store is out of stock. \n Thx Pirates!", new Vector2(20, 20), Color.LightGray, Color.Black);
        private DateTime timeOfRobbery;
        private int cooldownTime = 20000;
        private City city;
        private Vector2 mapPosition;
        private Vector2 shopPosition;
        private static bool[] shopCooldowns = new bool[] { false, false, false, false, false };

        internal Inventory Inventory
        {
            get
            {
                return GameWorld.Instance.Inventorys[(int)city];
            }

            set
            {
                GameWorld.Instance.Inventorys[(int)city] = value;
            }
        }

        public City City
        {
            get
            {
                return city;
            }

            set
            {
                city = value;
            }
        }

        public Vector2 MapPosition
        {
            get
            {
                return mapPosition;
            }

            set
            {
                mapPosition = value;
            }
        }

        public Vector2 ShopPosition
        {
            get
            {
                return shopPosition;
            }

            set
            {
                shopPosition = value;
            }
        }

        public OutsideLevel(string imageString, Vector2 spawnPoint, Tuple<int, int> boundaries, City city, Vector2 mapPosition, Vector2 shopPosition) : base(imageString, spawnPoint, boundaries)
        {
            this.City = city;
            this.MapPosition = mapPosition;
            this.ShopPosition = shopPosition;
        }

        public void ShopAction()
        {
            ks = Keyboard.GetState();
            Inventory inv = Inventory;

            if (inv.Items.Count > 0)
            {
                if (!ks.IsKeyDown(Keys.Space) && !shopCooldowns[(int)City])
                {
                    GameWorld.Instance.Tooltips.Add(t);

                    if ((DateTime.Now - timeOfRobbery).TotalMilliseconds > cooldownTime)
                    {
                        shopCooldowns[(int)City] = false;
                    }
                }
                else if (shopCooldowns[(int)City])
                {
                    GameWorld.Instance.Tooltips.Add(t1);
                }
                // loot store
                else
                {
                    // check store for items

                    // Select random item
                    Item i = inv.Items[r.Next(inv.Items.Count)];
                    // Select random amount of that item. - should be made more advanced
                    int count = r.Next(i.Count - 1) + 1;

                    GameWorld.Instance.MainInventory.AddItem(new Item(i.Name, count, i.Category));
                    Inventory.RemoveItem(i.Name, count);

                    shopCooldowns[(int)City] = true;

                    timeOfRobbery = DateTime.Now;

                }
            }
            else
            {
                GameWorld.Instance.Tooltips.Add(t2);
            }
        }

        public void MinionShopAction(Minion m)
        {
            Inventory inv = Inventory;

            if ((DateTime.Now - timeOfRobbery).TotalMilliseconds > cooldownTime)
            {
                shopCooldowns[(int)City] = false;
            }

            if (inv.Items.Count > 0 && !shopCooldowns[(int)City])
            {
                // Select random item
                Item i = inv.Items[r.Next(inv.Items.Count)];
                // Select random amount of that item based on minion strength
                int count = r.Next((int)Math.Round((i.Count - 1) * m.Strength/10)) + 1;

                GameWorld.Instance.MainInventory.AddItem(new Item(i.Name, count, i.Category));
                Inventory.RemoveItem(i.Name, count);

                shopCooldowns[(int)City] = true;

                timeOfRobbery = DateTime.Now;
            }
        }
    }
}
