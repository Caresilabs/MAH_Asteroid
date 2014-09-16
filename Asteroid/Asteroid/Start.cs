using System;
using System.Collections.Generic;
using System.Linq;
using Asteroid.Controller;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Asteroid
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Start : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Screen currentScreen;

        private Texture2D mouseTex;
        private Vector2 mousePosition;

        public Start()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferMultiSampling = false;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.PreferredBackBufferHeight= 720;

            // graphics.ToggleFullScreen();
           // Window.AllowUserResizing = true;
            Window.Title = "RETRO ALPHA MEGA DEMOLITION by Simon Bothen"; //  set title to our game name
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

        protected override void LoadContent()
        {
            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            Assets.load(Content);

            this.mouseTex = Assets.getTexture("Graphics/pointer");
            this.mousePosition = new Vector2();

            // init startup screen
            setScreen(getStartScreen());
        }

        protected override void UnloadContent()
        {
            // Unload any non ContentManager content here
            Assets.unload();
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // get second between last frame and current frame, used for fair physics manipulation and not based on frames
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //update mouse
            mousePosition.X = Mouse.GetState().X - mouseTex.Width / 2;
            mousePosition.Y = Mouse.GetState().Y - mouseTex.Height / 2;

            // then update the screen
            currentScreen.update(delta);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Draw screen
            currentScreen.draw(spriteBatch);

            spriteBatch.Begin();
            spriteBatch.Draw(mouseTex, mousePosition, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        internal void setScreen(Screen newScreen)
        {
            if (newScreen == null) return;

            // Dispose old screen
            if (currentScreen != null)
                currentScreen.dispose();

            // init new screen
            currentScreen = newScreen;
            newScreen.setGame(this);
            newScreen.setGraphics(GraphicsDevice);
            currentScreen.init();
        }

        private Screen getStartScreen()
        {
            return new IntroScene();
        }
    }
}
