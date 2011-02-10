#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Morito.ScreenManager;
#endregion


namespace Morito.Screens
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameplayScreen : GameScreen
    {
        #region Fields
        HumanPlayer _player1,_player2;               //the hasPosition2 has a single property for a 2d position, that I use for wrapping.
        #endregion

        #region Properties
        public HumanPlayer Player1
        {
            get { return _player1; }
            set { _player1 = value; }
        }
        #endregion

        #region Initialization
        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public GameplayScreen(string[] Level,string[] PlayerNumber,
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
            base.LoadContent();

            //create a new player and set the morito model to it.
            Player1 = new HumanPlayer(this, new Vector3(30, 20, 0)) { PlayersIndex = PlayerIndex.One };
            Player1.PlayersShip.ObjectModel = MoritoModel;
            //If we start having multiple meshes in our models, this may not work as intended.
            Player1.PlayersShip.BSphere = Player1.PlayersShip.ObjectModel.Meshes[0].BoundingSphere;
            Player1.PlayersShip.ReferenceCamera = Camera1;
            Collisions.Collidables.Add(Player1.PlayersShip);

            _player2 = new HumanPlayer(this, new Vector3(-30, -20, 0)) { PlayersIndex = PlayerIndex.Two };
            _player2.PlayersShip.ObjectModel = MoritoModel;
            //If we start having multiple meshes in our models, this may not work as intended.
            _player2.PlayersShip.BSphere = _player2.PlayersShip.ObjectModel.Meshes[0].BoundingSphere;
            _player2.PlayersShip.ReferenceCamera = Camera1;
            Collisions.Collidables.Add(_player2.PlayersShip);
            
            _objectsToBeWrapper.Add(Player1.PlayersShip);
            _objectsToBeWrapper.Add(_player2.PlayersShip);

            foreach (VisualObject3D piece in Level.Pieces)
            {
                _objectsToBeWrapper.Add(piece);
                //untested! Pieces that are collidable work, but untested with pieces that are not collidable. So untested if the the check works.
                if(piece is isCollidable)
                    Collisions.Collidables.Add((isCollidable)piece);
            }

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

            if (IsActive)
            {
                //Update the level
                this.Level.Update(gameTime);

                Player1.Update(gameTime);
                _player2.Update(gameTime);

                //wrapping! Works great for the ship... not so much for the asteroids... =P
                foreach (hasPosition2D anObject in _objectsToBeWrapper)
                {
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

                Collisions.update();
            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
            else
            {
              
            }

            if (keyboardState.IsKeyDown(Keys.Z))
                ScreenManager.AddScreen(new EndGameScreen(1), ControllingPlayer);

            if (keyboardState.IsKeyDown(Keys.X))
                ScreenManager.AddScreen(new EndGameScreen(2), ControllingPlayer);

        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            // draw the players
            Player1.Draw();
            _player2.Draw();
        }
        #endregion
    }
}
