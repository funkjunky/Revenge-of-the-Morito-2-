using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Morito
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MoritoFighterGame : Microsoft.Xna.Framework.Game
    {
        public static MoritoFighterGame MoritoFighterGameInstance;
      
        #region Member Variables
            //Native members
            GraphicsDeviceManager       _graphics;
            SpriteBatch                 _spriteBatchDrawable;

            //Screen Related
            public Morito.ScreenManager.ScreenManager _screenManager;
          
            Camera                      _camera1;               

            //Resources
            Texture2D                   _asteroidTexture;
            Texture2D                   _backgroundTexture;
            Model                       _moritoModel;
            Model                       _bulletModel;
            SpriteFont                  _fontSprite;
            Dictionary<string, string>  _displayedMessages;
        
            //Conviniences
            Random _rand;

            SoundEngine _sfxE;

        #endregion

        #region Constructors
            public MoritoFighterGame()
            {
                _screenManager = new Morito.ScreenManager.ScreenManager(this);
                Components.Add(_screenManager);
                _screenManager.AddScreen(new Morito.Screens.BackgroundScreen(), PlayerIndex.One);
                _screenManager.AddScreen(new Morito.Screens.MainMenuScreen(), PlayerIndex.One);
                //_screenManager.AddScreen(new Morito.Screens.DebugScreen(), PlayerIndex.One);
                Graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                MoritoFighterGameInstance = this;
/*

                _objectsToBeWrapper = new List<hasPosition2D>();
                
                Rand = new Random();
                Asteroids = new List<Asteroid>();
                Graphics = new GraphicsDeviceManager(this);
                Content.RootDirectory = "Content";
                DisplayedMessages = new Dictionary<string, string>();
                MoritoFighterGameInstance = this;*/
            }
        #endregion
        #region Properties
            public SoundEngine SfxE
            {
                get { return _sfxE; }
                set { _sfxE = value; }
            }

            public GraphicsDeviceManager Graphics
            {
                get { return _graphics; }
                set { _graphics = value; }
            }

            public SpriteBatch SpriteBatchDrawable
            {
                get { return _spriteBatchDrawable; }
                set { _spriteBatchDrawable = value; }
            }

            //

            public Camera Camera1
            {
                get { return _camera1; }
                set { _camera1 = value; }
            }

            //

            public Texture2D AsteroidTexture
            {
                get { return _asteroidTexture; }
                set { _asteroidTexture = value; }
            }

            public Texture2D BackgroundTexture
            {
                get { return _backgroundTexture; }
                set { _backgroundTexture = value; }
            }

            public SpriteFont FontSprite
            {
                get { return _fontSprite; }
                set { _fontSprite = value; }
            }

            public Dictionary<string, string> DisplayedMessages
            {
                get { return _screenManager.DisplayedMessages; }
                set { _screenManager.DisplayedMessages = value; }
            }

            public Model MoritoModel
            {
                get { return _moritoModel; }
                set { _moritoModel = value; }
            }

            public Model BulletModel
            {
                get { return _bulletModel; }
                set { _bulletModel = value; }
            }

            //

            public Random Rand
            {
                get { return _rand; }
                set { _rand = value; }
            }

            public Vector2 ScreenDimensions
            {
                get { 
                    return new Vector2(   Graphics.GraphicsDevice.PresentationParameters.BackBufferWidth, 
                                            Graphics.GraphicsDevice.PresentationParameters.BackBufferHeight
                                        ); 
                }
            }
        #endregion

        #region Public Methods
            /// <summary>
            /// Allows the game to perform any initialization it needs to before starting to run.
            /// This is where it can query for any required services and load any non-graphic
            /// related content.  Calling base.Initialize will enumerate through any components
            /// and initialize them as well.
            /// </summary>
            protected override void Initialize()
            {
                // Create a new SpriteBatch, which can be used to draw textures.
                SpriteBatchDrawable = new SpriteBatch(GraphicsDevice);

                base.Initialize();
            }

            /// <summary>
            /// LoadContent will be called once per game and is the place to load
            /// all of your content.
            /// </summary>
            protected override void LoadContent()
            {
                this.SfxE = new SoundEngine {   audioEngine = new AudioEngine(@"Content\Audio\GameAudio.xgs")};
                this.SfxE.waveBank = new WaveBank(SfxE.audioEngine, @"Content\Audio\Wave Bank.xwb");
                this.SfxE.soundBank = new SoundBank(SfxE.audioEngine, @"Content\Audio\Sound Bank.xsb");
               /* AsteroidTexture = Content.Load<Texture2D>("Texture\\Asteroid");
                BackgroundTexture = Content.Load<Texture2D>("Texture\\Space_BackGround");
                FontSprite = Content.Load<SpriteFont>(@"Fonts\Arial");

                MoritoModel = Content.Load<Model>("Models\\morito");
                BulletModel = Content.Load<Model>("Models\\missile");

                //setup a camera
                Camera1 = new Camera();

                //create a new player and set the morito model to it.
                Player1 = new HumanPlayer();
                Player1.PlayersShip.ObjectModel = MoritoModel;
                _objectsToBeWrapper.Add(Player1.PlayersShip);

                int numOfAsteroids = 10;
                for (int i = 0; i < numOfAsteroids; i++)
                {
                    Asteroid newAsteroid = new Asteroid
                    {
                        Texture = AsteroidTexture,
                        Position = ScreenDimensions * (float)Rand.NextDouble(),
                        Velocity = new Vector2((float)Rand.nextSignedDouble(), (float)Rand.nextSignedDouble()).goodNormalize() * (float)(Rand.NextDouble() * 2),
                        RotationSpeed = (float)Rand.nextSignedDouble() / 10
                    };
                    Asteroids.Add(newAsteroid);
                    _objectsToBeWrapper.Add(newAsteroid);
                }*/
            }

            /// <summary>
            /// UnloadContent will be called once per game and is the place to unload
            /// all content.
            /// </summary>
            protected override void UnloadContent()
            {
                // TODO: Unload any non ContentManager content here
                Content.Unload();
            }

            /// <summary>
            /// Allows the game to run logic such as updating the world,
            /// checking for collisions, gathering input, and playing audio.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            protected override void Update(GameTime gameTime)
            {
               /* //get the latest state of the keyboard
                Player1.GameKeyboard = Keyboard.GetState();

                // Allows the game to exit
                if (Player1.GameKeyboard.IsKeyDown(Keys.Escape))
                    this.Exit();

                //update all the game objects.
                foreach (Asteroid asteroid in Asteroids)
                {
                    asteroid.Update(gameTime);
                }
                Player1.Update(gameTime);

                //wrapping! Works great for the ship... not so much for the asteroids... =P
                foreach(hasPosition2D anObject in _objectsToBeWrapper) {
                    Vector2 position = anObject.Position2D;

                    if (position.X > (ScreenDimensions.X / 11f))
                        position.X = -(ScreenDimensions.X / 11f);
                    else if (position.X < -(ScreenDimensions.X / 11f))
                        position.X = (ScreenDimensions.X / 11f);
                    else if (position.Y > (ScreenDimensions.Y / 6f))
                        position.Y = -(ScreenDimensions.Y / 6f);
                    else if (position.Y < -(ScreenDimensions.Y / 6f))
                        position.Y = (ScreenDimensions.Y / 6f);

                    anObject.Position2D = position;
                }
                */
                base.Update(gameTime);
            }

            /// <summary>
            /// This is called when the game should draw itself.
            /// </summary>
            /// <param name="gameTime">Provides a snapshot of timing values.</param>
            protected override void Draw(GameTime gameTime)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                /*Rectangle screenRectangle = new Rectangle(0, 0, Graphics.GraphicsDevice.PresentationParameters.BackBufferWidth, Graphics.GraphicsDevice.PresentationParameters.BackBufferHeight);
                SpriteBatchDrawable.Begin();
                    SpriteBatchDrawable.Draw(BackgroundTexture, screenRectangle, Color.LightGray);
                SpriteBatchDrawable.End();

                //draw the asteroids
                foreach (Asteroid asteroid in Asteroids)
                {
                    asteroid.Draw();
                }

                //draw the player
                Player1.Draw();

                //draw all messages.
                SpriteBatchDrawable.Begin();
                    SpriteBatchDrawable.DrawString(FontSprite, string.Join("\n", DisplayedMessages.Values.ToArray()), new Vector2(0.0f, 0.0f), Color.White);
                SpriteBatchDrawable.End();
                */
                base.Draw(gameTime);
            }
        #endregion
    }
}
