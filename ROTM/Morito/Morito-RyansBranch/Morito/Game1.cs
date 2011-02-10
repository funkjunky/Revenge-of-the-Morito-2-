using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace Morito
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Asteroid> myAstroids=new List<Asteroid>();
        Texture2D AstroidTexture,BackgroundTexture;
        Random rand = new Random();
        Background background;
        Player Player1;
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
            // TODO: Add your initialization logic here

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
            Player1 = new Player(this,Content.Load<Texture2D>("Texture\\Il-Wrath"),
                                      Content.Load<Texture2D>("Texture\\planet"),WeaponTypes.Basic);
            AstroidTexture = Content.Load<Texture2D>("Texture\\Asteroid");
            BackgroundTexture = Content.Load<Texture2D>("Texture\\Space_BackGround");
            background = new Background(BackgroundTexture,graphics.GraphicsDevice.PresentationParameters.BackBufferWidth,
                graphics.GraphicsDevice.PresentationParameters.BackBufferHeight);
            for (int i = 0; i < 10; i++)
            {
                myAstroids.Add(new Asteroid(this, AstroidTexture, new Vector2((float)rand.NextDouble() * graphics.GraphicsDevice.PresentationParameters.BackBufferWidth, (float)rand.NextDouble() * graphics.GraphicsDevice.PresentationParameters.BackBufferWidth),
                                                1f,
                                                0.0f, new Vector2(AstroidTexture.Width / 2, AstroidTexture.Height / 2), spriteBatch,
                                                10f, 10f, 4f));
            }
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            KeyboardState keyboard = Keyboard.GetState();
            foreach (Asteroid myAstroid in myAstroids)
            {
                myAstroid.Update(gameTime);
            }
            Player1.Keyboard = keyboard;
            Player1.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            background.Draw(spriteBatch);
            foreach (Asteroid myAstroid in myAstroids)
            {
                myAstroid.Draw(spriteBatch);
            }
            Player1.Draw(spriteBatch);
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
