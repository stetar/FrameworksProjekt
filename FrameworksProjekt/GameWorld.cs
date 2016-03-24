﻿using System.Collections.Generic;
using FrameworksProjekt.Builder;
using FrameworksProjekt.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FrameworksProjekt.Items;
using System;
using System.Linq;

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

        // Testing
        // Log mouse position in debug and show mouse
        bool logMouse = false;

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

        private GameWorld()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Camera = new Camera(Vector2.Zero);
            Graphics.PreferredBackBufferWidth = 1422;
            Graphics.PreferredBackBufferHeight = 800;
            Inventorys = new Inventory[5];
            spawner = new Spawner();
            MainInventory = new Inventory();
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

            this.Colliders = new List<Collider>();
            this.tooltips = new List<Tooltip>();
            this.recruits = new List<Minion>();

            if(logMouse)
            {
                this.IsMouseVisible = true;
            }

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
            int height = 100;
            int width = 100;
            Vector2 startPos = new Vector2(900, 200);

            for(int i = 0; i < mainInventory.Items.Count; i++)
            {
                Item it = mainInventory.Items[i];
                float x = startPos.X + width * ( i % 4 );
                float y = startPos.Y + height * (float)Math.Floor(i / 4d);

                spriteBatch.Draw(it.Sprite, new Vector2(x, y));
                spriteBatch.DrawString(StandardFont, it.Name, new Vector2(x, y + 64), Color.Black);
                spriteBatch.DrawString(StandardFont, "x"+it.Count, new Vector2(x, y + 80), Color.Black);
            }
        }   
    }
}
