using System.Collections.Generic;
using FrameworksProjekt.Builder;
using FrameworksProjekt.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FrameworksProjekt.Items;
using System;
using System.Linq;
using FrameworksProjekt.Database;
using System.Data.SQLite;
using FrameworksProjekt.Database.Factories;

namespace FrameworksProjekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    public enum SpawnRoom
    {
        Cellar,
        VeganStore,
        CoffeeShop
    }

    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<GameObject> gameObjects;
        private Level gameLevel;
        private Camera camera;
        private Rectangle displayRect;
        private List<Minion> minions;
        private List<Collider> colliders;
        private GameObject player;
        private List<Tooltip> tooltips;
        private SpriteFont standardFont;
        // inventorys of all shops ordered by the city enum in level.cs 
        private Inventory[] inventorys;
        // headquarter inventory
        private Inventory mainInventory;
        private Spawner spawner;
        private bool drawInventory;
        private static Random r = new Random();
        private static GameWorld instance;
        private List<Minion> recruits;
        // Minion that player is currently sending out to loot or sending to train
        private Minion activeMinion;
        private DatabaseMan databaseManager;
        private bool shouldLoad;
        private bool animateRadio;
        private DateTime radioAnimationStart;
        private Texture2D[] radioFrames = new Texture2D[5];

        private InventoryFac infa = new InventoryFac();
        private ItemFac itfa = new ItemFac();
        private MinionFac mifa = new MinionFac();

        // Testing
        // Log mouse position in debug and show mouse
        bool logMouse = true;

        public float Delta { get; set; }

        public static GameWorld Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameWorld();
                }

                return instance;
            }
        }

        public List<Minion> Minions
        {
            get { return minions; }
            set { minions = value; }
        }

        public Camera Camera
        {
            get
            {
                return camera;
            }

            set
            {
                camera = value;
            }
        }

        public Rectangle DisplayRect
        {
            get
            {
                return displayRect;
            }

            set
            {
                displayRect = value;
            }
        }

        internal Level GameLevel
        {
            get
            {
                return gameLevel;
            }
            set
            {
                gameLevel = value;
            }
        }

        public List<Collider> Colliders
        {
            get { return colliders; }
            set { colliders = value; }
        }

        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        public GameObject Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }

        internal List<Tooltip> Tooltips
        {
            get
            {
                return tooltips;
            }

            set
            {
                tooltips = value;
            }
        }

        public SpriteFont StandardFont
        {
            get
            {
                return standardFont;
            }

            set
            {
                standardFont = value;
            }
        }

        public GraphicsDeviceManager Graphics
        {
            get
            {
                return graphics;
            }

            set
            {
                graphics = value;
            }
        }

        public Inventory[] Inventorys
        {
            get
            {
                return inventorys;
            }

            set
            {
                inventorys = value;
            }
        }

        public Inventory MainInventory
        {
            get
            {
                return mainInventory;
            }

            set
            {
                mainInventory = value;
            }
        }

        public bool DrawInventory
        {
            get
            {
                return drawInventory;
            }

            set
            {
                drawInventory = value;
            }
        }

        public List<Minion> Recruits
        {
            get
            {
                return recruits;
            }

            set
            {
                recruits = value;
            }
        }

        public Minion ActiveMinion
        {
            get
            {
                return activeMinion;
            }

            set
            {
                activeMinion = value;
            }
        }

        public bool ShouldLoad
        {
            get
            {
                return shouldLoad;
            }

            set
            {
                shouldLoad = value;
            }
        }

        public bool AnimateRadio
        {
            get
            {
                return animateRadio;
            }

            set
            {
                animateRadio = value;
                radioAnimationStart = DateTime.Now;
            }
        }

        private GameWorld()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Camera = new Camera(Vector2.Zero);
            Graphics.PreferredBackBufferWidth = 1422;
            Graphics.PreferredBackBufferHeight = 800;
            shouldLoad = false;
            AnimateRadio = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gameObjects = new List<GameObject>();

            LevelDirector LD  = new LevelDirector(new HeadQuartersBuilder());
            gameLevel = LD.Construct();

            Inventorys = new Inventory[5];
            MainInventory = new Inventory();

            this.Colliders = new List<Collider>();
            this.tooltips = new List<Tooltip>();
            this.recruits = new List<Minion>();

            if (logMouse)
            {
                this.IsMouseVisible = true;
            }

            this.databaseManager = new DatabaseMan();
            databaseManager.SetUp();

            spawner = new Spawner();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            standardFont = Content.Load<SpriteFont>("StandardFont");

            this.spawner.Init();

            if (shouldLoad)
            {
                LoadSave();
            }

            LoadRadioFrames();

            this.GameLevel.LoadContent(Content);

            foreach (GameObject obj in gameObjects)
            {
                obj.LoadContent(Content);
            }

            DisplayRect = GraphicsDevice.Viewport.Bounds;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            tooltips.Clear();
            drawInventory = false;

            spawner.Update();

            Delta = (float) gameTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here

            spriteBatch.Begin();

            gameLevel.Draw(spriteBatch);

            if (AnimateRadio && gameLevel.Name == "Headquarters")
                DrawRadio(spriteBatch);

            foreach (GameObject obj in gameObjects)
            {
                Minion m = (Minion)obj.GetComponent("Minion");

                // Only draw minions if in current gamelevel
                if ( m == null )
                {
                    obj.Draw(spriteBatch);
                }
                else if(gameLevel.Name == m.CurrentLevel.Name)
                {
                    obj.Draw(spriteBatch);                 
                }
            }

            foreach(Tooltip t in tooltips)
            {
                t.Draw(spriteBatch);
            }

            if (drawInventory)
                DrawMainInventory(spriteBatch);

            if(logMouse)
            {
                System.Diagnostics.Debug.WriteLine((Mouse.GetState().X + camera.Position.X)+", " + (Mouse.GetState().Y + camera.Position.Y));
            }
    
            spriteBatch.End();
            base.Draw(gameTime);
        }

        protected override void OnExiting(Object sender, EventArgs e)
        {
            SaveGame();
        }

        public void LoadLevel(Level l)
        {
            l.LoadContent(Content);
        }

        private void GenerateMinion(SpawnRoom spawn)
        {
            GameObjectDirector d = new GameObjectDirector(new MinionBuilder());
            LevelDirector ld;
            // minion
            GameObject g = d.Construct();
            Level l;

            switch ((int)spawn)
            {
                case 1:
                    ld = new LevelDirector(new CellarBuilder());
                    break;

                case 2:
                    ld = new LevelDirector(new VeganStoreBuilder());
                    break;

                case 3:
                    ld = new LevelDirector(new CoffeeshopBuilder());
                    break;

                default:
                    ld = new LevelDirector(new CellarBuilder());
                    break;
            }

            l = ld.Construct();
            l.LoadContent(GameWorld.Instance.Content);
            ((Minion)g.GetComponent("Minion")).CurrentLevel = l;

            g.GetTransform.Position = new Vector2(r.Next(l.Boundaries.Item1, l.Boundaries.Item2 - 128), l.SpawnPoint.Y);

            g.LoadContent(Content);

            gameObjects.Add(g);
        }

        private void DrawMainInventory(SpriteBatch spriteBatch)
        {
            int height = 120;
            int width = 100;
            Vector2 startPos = new Vector2(900, 100);

            for(int i = 0; i < mainInventory.Items.Count; i++)
            {
                Item it = mainInventory.Items[i];
                float x = startPos.X + width * ( i % 4 );
                float y = startPos.Y + height * (float)Math.Floor(i / 4d);

                spriteBatch.Draw(it.Sprite, new Vector2(x, y));
                spriteBatch.DrawString(StandardFont, it.Name.Replace(" ", "\n"), new Vector2(x, y + 64), Color.Black);
                spriteBatch.DrawString(StandardFont, "x"+it.Count, new Vector2(x, y + 100), Color.Black);
            }
        }   

        private void SaveGame()
        {
            databaseManager.RecreateMinionTable();

            databaseManager.RecreateInventoryTable();

            foreach(Minion m in Recruits)
            {
                m.SaveMinion();
            }

            foreach(Item i in mainInventory.Items)
            {
                //create new inventory line/item in database
                Database.Types.Inventory iI = new Database.Types.Inventory();
                iI.Item = itfa.GetBy("Name", i.Name)[0].ID;
                iI.Count = i.Count;
                iI.Name = i.Name;

                infa.Insert(iI);
            }
        }

        public void LoadSave()
        {
            using (var conn = new SQLiteCommand(Conn.CreateConnection()))
            {
                // Load items
                foreach (Database.Types.Inventory iI in infa.GetAll())
                {
                    mainInventory.AddItem(new Item(iI.Name, (int)iI.Count, (Category)itfa.Get(iI.ID).Category));
                }

                foreach (Database.Types.Minion m in mifa.GetAll())
                {
                    GameObjectDirector GOD = new GameObjectDirector(new MinionBuilder());
                    GameObject g = GOD.Construct();
                    Minion mn = (Minion)g.GetComponent("Minion");

                    mn.CurrentLevel = CreateGameLevel(m.currentLevelName);
                    mn.TargetLevel = CreateGameLevel(m.targetLevelName);
                    mn.Speed = (float)m.Speed;
                    mn.Strength = (float)m.Strength;
                    mn.Wild = false;
                    g.GetTransform.Position = new Vector2((float)m.X, (float)m.Y);
                    ((SpriteRenderer)g.GetComponent("SpriteRenderer")).SpriteName = m.imagePath;

                    recruits.Add(mn);
                    gameObjects.Add(g);
                }
            }
        }

        private Level CreateGameLevel(string name)
        {
            Level l;

            switch(name)
            {
                case "Cellar":
                    l = new InsideLevel("Cellar", new Vector2(600, 500), new Tuple<int, int>(350, 100), new Vector2(1322, 550), new OutsideLevel("Grenaa", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Grenaa, new Vector2(50, 500), new Vector2(1650, 550)));
                    break;

                case "CoffeeShop":
                    l = new InsideLevel("Cellar", new Vector2(500, 500), new Tuple<int, int>(400, 100), new Vector2(1322, 550), new OutsideLevel("Aarhus", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Aarhus, new Vector2(50, 500), new Vector2(1660, 550)));
                    break;

                case "Esbjerg":
                    l = new OutsideLevel("Esbjerg", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Aarhus, new Vector2(50, 500), new Vector2(1660, 550));
                    break;

                case "Grenaa":
                    l = new OutsideLevel("Grenaa", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Grenaa, new Vector2(50, 500), new Vector2(1650, 550));
                    break;

                case "Headquarters":
                    l = new InsideLevel("Headquarters", new Vector2(350, 500), new Tuple<int, int>(240, -100), new Vector2(260, 550), new OutsideLevel("Grenaa", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Grenaa, new Vector2(50, 500), new Vector2(1650, 550)));
                    break;

                case "København":
                    l = new OutsideLevel("København", new Vector2(120, 500), new Tuple<int, int>(-120, -120), City.København, new Vector2(50, 500), new Vector2(1605, 550));
                    break;

                case "Skagen":
                    l = new OutsideLevel("Skagen", new Vector2(0, 500), new Tuple<int, int>(-120, -120), City.Skagen, new Vector2(50, 500), new Vector2(1605, 550));
                    break;

                case "Vegan store":
                    l = new InsideLevel("Vegan store", new Vector2(0, 500), new Tuple<int, int>(400, 100), new Vector2(50, 550), new OutsideLevel("København", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.København, new Vector2(50, 500), new Vector2(1605, 550)));
                    break;

                case "Aarhus":
                    l = new OutsideLevel("Aarhus", new Vector2(120, 500), new Tuple<int, int>(-120, -120), City.Aarhus, new Vector2(50, 500), new Vector2(1660, 550));
                    break;

                default:
                    l = new InsideLevel("Headquarters", new Vector2(350, 500), new Tuple<int, int>(240, -100), new Vector2(260, 550), new OutsideLevel("Grenaa", new Vector2(20, 500), new Tuple<int, int>(-120, -120), City.Grenaa, new Vector2(50, 500), new Vector2(1650, 550)));
                    break;
            }

            return l;
        }

        public void DrawRadio(SpriteBatch spriteBatch)
        {
            int delta = (int)(DateTime.Now - radioAnimationStart).TotalMilliseconds;
            int delay = 2000;
            Vector2 position = new Vector2(680, 260);

            if(delta <= delay * radioFrames.Length )
            {
                Texture2D sprite = radioFrames[delta / delay];
                //float scale = ((((delta % 1000) / 500) * 2) - 2) + 1;
                spriteBatch.Draw(sprite, position, new Rectangle(0, 0, sprite.Width, sprite.Height), Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 1);
            }
            else
            {
                animateRadio = false;
            }
        }

        public void LoadRadioFrames()
        {
            radioFrames[0] = Content.Load<Texture2D>("Apple");
            radioFrames[1] = Content.Load<Texture2D>("CheeseBurger");
            radioFrames[2] = Content.Load<Texture2D>("Cloths");
            radioFrames[3] = Content.Load<Texture2D>("Electronics");
            radioFrames[4] = Content.Load<Texture2D>("Sofa");
        }
    }
}
