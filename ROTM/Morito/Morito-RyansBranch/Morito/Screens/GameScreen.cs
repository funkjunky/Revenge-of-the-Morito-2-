#region Using Statements
using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Morito.ScreenManager;
using Morito.Classes;
#endregion


namespace Morito.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameScreen : Screen
    {
        #region Fields
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatchDrawable;

        protected ContentManager content;

        CollisionEngine _collisions;

        Level _level;
        //Game Object Members
        protected List<hasPosition2D> _objectsToBeWrapped;    //I initially intended this to be a list of all objects, 
        //but I just need objects to wrap
        //List<Asteroid> _asteroids;             //and having something of all objects requires more thought.
        
        Camera _camera1;               //visualObject2D and 3d both implement it.
        //THIS IS A TEMPORARY HACK. I didn't even bother making a property for it.

        //Resources
        //Texture2D _backgroundTexture;

        Model _asteroidModel;
        Model _moritoModel;
        Model _bulletModel;

        Texture2D _asteroidTexture;

        SpriteFont _fontSprite;
        Dictionary<string, string> _displayedMessages;

        //Conviniences
        //Random _rand;

        #endregion

        #region Properties
        public List<hasPosition2D> ObjectsToBeWrapped
        {
            get { return _objectsToBeWrapped; }
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
        public CollisionEngine Collisions
        {
            get { return _collisions; }
            set { _collisions = value; }
        }

        public Level Level
        {
            get { return _level; }
            set { _level = value; }
        }

        //public List<Asteroid> Asteroids
        //{
        //    get { return _asteroids; }
        //    set { _asteroids = value; }
        //}

        public Camera Camera1
        {
            get { return MoritoFighterGame.MoritoFighterGameInstance.Camera1; }
            set { MoritoFighterGame.MoritoFighterGameInstance.Camera1 = value; }
        }

        //

        //public Texture2D BackgroundTexture
        //{
        //    get { return _backgroundTexture; }
        //    set { _backgroundTexture = value; }
        //}

        public SpriteFont FontSprite
        {
            get { return _fontSprite; }
            set { _fontSprite = value; }
        }

        public Dictionary<string, string> DisplayedMessages
        {
            get { return ScreenManager.DisplayedMessages; }
            set { ScreenManager.DisplayedMessages = value; }
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
        public Model AsteroidModel
        {
            get { return _asteroidModel; }
            set { _asteroidModel = value; }
        }

        public Texture2D AsteroidTexture
        {
            get { return _asteroidTexture; }
            set { _asteroidTexture = value; }
        }
        //

        //public Random Rand
        //{
        //    get { return _rand; }
        //    set { _rand = value; }
        //}

        public Vector2 ScreenDimensions
        {
            get
            {
                return new Vector2(ScreenManager.Game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                                        ScreenManager.Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                                    );
            }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor.
        /// </summary>
        public GameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public GameScreen(string[] Level,string[] PlayerNumber,
             int CurrentLevel,int CurrentPlayerNumber)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            //TODO: REMOVE ME Once Level is added!
            Console.WriteLine("GameScreenGot: +Level:" + Level[CurrentLevel] + " Player#:" + PlayerNumber[CurrentPlayerNumber]
                + "Level#:" + CurrentLevel
 + "CurrentPlayerNumber:" + CurrentPlayerNumber);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            _collisions = new CollisionEngine();

            _objectsToBeWrapped = new List<hasPosition2D>();

            DisplayedMessages = new Dictionary<string, string>();
            SpriteBatchDrawable = new SpriteBatch(ScreenManager.Game.GraphicsDevice);
           
            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            // Note by Jason: Awesome... lol
            Thread.Sleep(1000);
            
            FontSprite = ScreenManager.Game.Content.Load<SpriteFont>("Fonts\\Arial");

            MoritoModel = ScreenManager.Game.Content.Load<Model>("Models\\morito");           
            BulletModel = ScreenManager.Game.Content.Load<Model>("Models\\missile");                  

            //setup a camera
            Camera1 = new Camera();

            Level = new Level();
            Level.LoadContent(this);
            //Level.saveLevelToFile(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\MoritoLevel.xml");

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            content.Unload();
        }
        #endregion

        #region Update and Draw
        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            //nothing to do on base, but I think this function may be necessary.
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            //Draw the level
            this.Level.Draw(gameTime);

            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }
        #endregion
    }
}
