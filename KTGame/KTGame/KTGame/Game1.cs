using KTGame.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using KTGame.Commands;
using KTGame.Controllers;
using KTGame.Factories;
using KTGame.Interfaces;
using KTGame.Objects;
using KTGame.Objects.BlockObjects;
using KTGame.Objects.Enemies;
using KTGame.Objects.Items;
using KTGame.Sprites;
using KTGame.Sprites.Items;
using System;

namespace KTGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        SpriteBatch spriteBatch;
        readonly GraphicsDeviceManager graphics;

        private LevelLoader levelLoader;
        private MarioWorld marioWorld;
        private ISprite BackgroundSprite;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
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
            Texture2D backgroundTexture = Content.Load<Texture2D>("StructuresSprite");
            BackgroundSprite = new BackgroundSprite(backgroundTexture);
            levelLoader = new LevelLoader();

            marioWorld = levelLoader.LoadWorld("Sprint5.xml", this.Content, this, BackgroundSprite);

        
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
            marioWorld.Update(gameTime);
            base.Update(gameTime);
            
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            // TODO: Add your drawing code here
           marioWorld.Draw(spriteBatch);
           base.Draw(gameTime);

        }
        public void Reset()
        {
            marioWorld = levelLoader.LoadWorld("Sprint5.xml", this.Content, this, BackgroundSprite);
            HUD.Instance.ResetAll();
        }
    }
}
