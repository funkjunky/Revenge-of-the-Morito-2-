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
        int players;
        HUD hudManager = new HUD();
        HumanPlayer _player1,_player2,_player3,_player4;               //the hasPosition2 has a single property for a 2d position, that I use for wrapping.
        #endregion

        #region Properties
        public HumanPlayer Player1
        {
            get { return _player1; }
            set { _player1 = value; }
        }

        public HumanPlayer Player2
        {
            get { return _player2; }
            set { _player2 = value; }
        }

        public HumanPlayer Player3
        {
            get { return _player3; }
            set { _player3 = value; }
        }

        public HumanPlayer Player4
        {
            get { return _player4; }
            set { _player4 = value; }
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
            players = CurrentPlayerNumber;
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

            //TODO: REMOVE ME Once Level is added!
            Console.WriteLine("GameScreenGot: +Level:" + Level[CurrentLevel] + " Player#:" + PlayerNumber[CurrentPlayerNumber-1]
                + "Level#:" + CurrentLevel
 + "CurrentPlayerNumber:" + CurrentPlayerNumber);
        }

        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            base.LoadContent();

            if (players >= 1)
            {

                //create a new player and set the morito model to it.
                Player1 = new HumanPlayer(this, new Vector3(30, 20, 0)) { PlayersIndex = PlayerIndex.One };
                Player1.PlayersShip.ObjectModel = MoritoModel;
                //If we start having multiple meshes in our models, this may not work as intended.
                Player1.PlayersShip.BSphere = Player1.PlayersShip.ObjectModel.Meshes[0].BoundingSphere;
                Player1.PlayersShip.ReferenceCamera = Camera1;
                Collisions.Collidables.Add(Player1.PlayersShip);
                _objectsToBeWrapped.Add(Player1.PlayersShip);
            }

            if (players >= 2)
            {
                Player2 = new HumanPlayer(this, new Vector3(-30, -20, 0)) { PlayersIndex = PlayerIndex.Two };
                Player2.PlayersShip.ObjectModel = MoritoModel;
                //If we start having multiple meshes in our models, this may not work as intended.
                Player2.PlayersShip.BSphere = Player2.PlayersShip.ObjectModel.Meshes[0].BoundingSphere;
                Player2.PlayersShip.ReferenceCamera = Camera1;
                Collisions.Collidables.Add(Player2.PlayersShip);
                _objectsToBeWrapped.Add(Player2.PlayersShip);
            }

            if (players >= 3)
            {
                Player3 = new HumanPlayer(this, new Vector3(-50, -60, 0)) { PlayersIndex = PlayerIndex.Three };
                Player3.PlayersShip.ObjectModel = MoritoModel;
                //If we start having multiple meshes in our models, this may not work as intended.
                Player3.PlayersShip.BSphere = Player3.PlayersShip.ObjectModel.Meshes[0].BoundingSphere;
                Player3.PlayersShip.ReferenceCamera = Camera1;
                Collisions.Collidables.Add(Player3.PlayersShip);
                _objectsToBeWrapped.Add(Player3.PlayersShip);
            }

            if (players == 4)
            {
                Player4 = new HumanPlayer(this, new Vector3(50, 60, 0)) { PlayersIndex = PlayerIndex.Four };
                Player4.PlayersShip.ObjectModel = MoritoModel;
                //If we start having multiple meshes in our models, this may not work as intended.
                Player4.PlayersShip.BSphere = Player4.PlayersShip.ObjectModel.Meshes[0].BoundingSphere;
                Player4.PlayersShip.ReferenceCamera = Camera1;
                Collisions.Collidables.Add(Player4.PlayersShip);
                _objectsToBeWrapped.Add(Player4.PlayersShip);
            }

            foreach (VisualObject3D piece in Level.Pieces)
            {
                _objectsToBeWrapped.Add(piece);
                //untested! Pieces that are collidable work, but untested with pieces that are not collidable. So untested if the the check works.
                if(piece is isCollidable)
                    Collisions.Collidables.Add((isCollidable)piece);
            }

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();

            // Once all players have been created pass the screen to the hudManager to have
            // set up HUDs for active players
            hudManager.PlayerOne = Player1;
            hudManager.PlayerTwo = Player2;
            hudManager.PlayerThree = Player3;
            hudManager.PlayerFour = Player4;
            hudManager.initilizeHUD(this);
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

                if (Player1 != null)
                {
                    Player1.Update(gameTime);
                }
                if (Player2 != null)
                {
                    Player2.Update(gameTime);
                }

                if (Player3 != null)
                {
                    Player3.Update(gameTime);
                }

                if (Player4 != null)
                {
                    Player4.Update(gameTime);
                }

                //currently our orgin is in the middle
                //if we move it to the bottom left corner
                //then we can use the below code
                //position.X = (position.X % (ScreenDimensions.X / 8.5f));
                //position.Y = (position.Y % (ScreenDimensions.Y / 8.5f));
                foreach (hasPosition2D anObject in _objectsToBeWrapped)
                {
                    Vector2 position = anObject.Position2D;

                    if (position.X > (ScreenDimensions.X / 8.55f))
                        position.X = -(ScreenDimensions.X / 8.55f);
                    else if (position.X < -(ScreenDimensions.X / 8.55f))
                        position.X = (ScreenDimensions.X / 8.55f);
                    else if (position.Y > (ScreenDimensions.Y / 8.55f))
                        position.Y = -(ScreenDimensions.Y / 8.55f);
                    else if (position.Y < -(ScreenDimensions.Y / 8.55f))
                        position.Y = (ScreenDimensions.Y / 8.55f);

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

            // draw the huds
            hudManager.Draw();

            // draw the players
            if (Player1 != null)
            {
                Player1.Draw();
            }
            if (Player2 != null)
            {
                Player2.Draw();
            }
            if (Player3 != null)
            {
                Player3.Draw();
            }
            if (Player4 != null)
            {
                Player4.Draw();
            }
        }
        #endregion
    }
}
