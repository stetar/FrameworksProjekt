using System.Collections.Generic;
using FrameworksProjekt.Builder;
using FrameworksProjekt.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FrameworksProjekt
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class GameWorld : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private List<GameObject> gameObjects;
        private Level gameLevel;
        private Camera camera;
        private GameObject player;
        private Rectangle displayRect;
        private List<Minion> minions;
        private List<Collider> colliders; 

        public float Delta { get; set; }
        private static GameWorld instance;

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

        public List<GameObject> GameObjects
        {
            get { return gameObjects; }
            set { gameObjects = value; }
        }

        private GameWorld()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Camera = new Camera(Vector2.Zero);
            graphics.PreferredBackBufferWidth = 1422;
            graphics.PreferredBackBufferHeight = 800;
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
            GameObjectDirector GOD = new GameObjectDirector(new PlayerBuilder());
            GameObject player = GOD.Construct();
            gameObjects.Add(player);
            this.player = player;
            Colliders = new List<Collider>();

            LevelDirector LD  = new LevelDirector(new AarhusBuilder());
            gameLevel = LD.Construct();

            this.Colliders = new List<Collider>();

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
            Delta = (float) gameTime.ElapsedGameTime.TotalSeconds;
            foreach (GameObject obj in gameObjects)
            {
                obj.Update();
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
                obj.Draw(spriteBatch);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void LoadLevel(Level l)
        {
            l.LoadContent(Content);
        }
    }
}
