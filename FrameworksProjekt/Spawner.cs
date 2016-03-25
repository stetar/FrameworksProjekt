using FrameworksProjekt.Builder;
using FrameworksProjekt.Components;
using FrameworksProjekt.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FrameworksProjekt
{
    class Spawner
    {
        private static Random r = new Random();
        private ItemGenerator it;
        private GameObjectDirector GOD;
        private DateTime lastSpawn;
        private int timeToSpawn = 600000;

        public Spawner()
        {
            
        }

        public void Init()
        {
            it = new ItemGenerator();
            
            ResetInventories();
            SpawnPlayer();
            SpawnChubby();
            SpawnHipster();
            SpawnVegan();

            lastSpawn = DateTime.Now;
        }

        public void SpawnPlayer()
        {
            GOD = new GameObjectDirector(new PlayerBuilder());
            GameWorld.Instance.GameObjects.Add(GOD.Construct());
        }

        public void SpawnVegan()
        {
            GenerateMinion(SpawnRoom.VeganStore);
        }

        public void SpawnChubby()
        {
            GenerateMinion(SpawnRoom.Cellar);
        }

        public void SpawnHipster()
        {
            GenerateMinion(SpawnRoom.CoffeeShop);
        }

        public void Update()
        {
            if((lastSpawn-DateTime.Now).TotalMilliseconds > timeToSpawn)
            {

            }
        }

        private void GenerateMinion(SpawnRoom spawn)
        {
            GOD = new GameObjectDirector(new MinionBuilder());
            LevelDirector ld;
            // minion
            GameObject g = GOD.Construct();
            Level l;

            switch ((int)spawn)
            {
                case 0:
                    ld = new LevelDirector(new CellarBuilder());
                    break;

                case 1:
                    ld = new LevelDirector(new VeganStoreBuilder());
                    break;

                case 2:
                    ld = new LevelDirector(new CoffeeshopBuilder());
                    break;

                default:
                    ld = new LevelDirector(new CellarBuilder());
                    break;
            }

            l = ld.Construct();
            l.LoadContent(GameWorld.Instance.Content);
            Minion m = ((Minion)g.GetComponent("Minion"));
            m.CurrentLevel = l;
            m.Type = (Miniontype)spawn;

            g.GetTransform.Position = new Vector2(r.Next(l.Boundaries.Item1, l.Boundaries.Item2 - 128), l.SpawnPoint.Y);

            g.LoadContent(GameWorld.Instance.Content);

            GameWorld.Instance.GameObjects.Add(g);
        }


        private void ResetInventories()
        {
            Inventory[] Inventorys = GameWorld.Instance.Inventorys;
            Inventorys = new Inventory[5];

            for (int i = 0; i < Inventorys.Length; i++)
            {
                Inventorys[i] = new Inventory();
                Inventorys[i].AddItem(it.GenerateItem((Category)i, 5));
            }

            GameWorld.Instance.Inventorys = Inventorys;
        }

        public void AddItemToInventories()
        {
            Inventory[] Inventorys = GameWorld.Instance.Inventorys;

            for (int i = 0; i < Inventorys.Length; i++)
            {
                Inventorys[i].AddItem(it.GenerateItem((Category)i, 5));
            }
        }
    }
}
