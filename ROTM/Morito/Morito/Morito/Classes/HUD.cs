using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Morito
{
    public class HUD
    {
        #region Private Member Variables
        // HUD variables
        Dictionary<string, string> hudMessages;
        // Stats pointers are used later for when player hold weps and when the HUD becomes
        // advanced enough to display HP. Nicer looking graphics needed.
        private HumanPlayer _playerOneHUD, _playerTwoHUD, _playerThreeHUD, _playerFourHUD;

        // Heads Up Display for each player
        private VisualObject2D _player1HUDAlive, _player2HUDAlive, _player3HUDAlive, _player4HUDAlive;

        // Weapon and Health Points for each players HUD
        private VisualObject2D _player1HUDWep, _player2HUDWep, _player3HUDWep, _player4HUDWep;
        private Vector2 p1HP, p2HP, p3HP, p4HP;
        SpriteBatch _playerHP;

        // HUD Font
        SpriteFont font;

        // Game screen pointer
        private Screens.GameplayScreen _gameScreen;
        private ScreenManager.ScreenManager _screenManager;
        
        #endregion

        #region Constructor and Initilizer
        public HUD()
        {
            hudMessages = MoritoFighterGame.MoritoFighterGameInstance._screenManager.DisplayedMessages;
        }

        public void initilizeHUD(Screens.GameplayScreen screen)
        {
            GameScreen = screen;
            _screenManager = screen.ScreenManager;
            _playerHP = _screenManager.SpriteBatch;
            font = GameScreen.ScreenManager.Game.Content.Load<SpriteFont>("Fonts\\monofont");

            if (PlayerOne != null)
            {
                _player1HUDAlive = new VisualObject2D();
                _player1HUDAlive.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\Il-Wrath");
                _player1HUDAlive.Position = new Vector2(10, 10);
                _player1HUDAlive.RotationAngle = 0;
                _player1HUDAlive.Origin = new Vector2(0,0);
                _player1HUDAlive.Scale = 0.5f;

                p1HP = new Vector2(60, 10);
            }

            if (PlayerTwo != null)
            {
                _player2HUDAlive = new VisualObject2D();
                _player2HUDAlive.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\Il-Wrath");
                _player2HUDAlive.Position = new Vector2(GameScreen.ScreenDimensions.X-50, 10);
                _player2HUDAlive.RotationAngle = 0;
                _player2HUDAlive.Origin = new Vector2(0, 0);
                _player2HUDAlive.Scale = 0.5f;

                p2HP = new Vector2(GameScreen.ScreenDimensions.X-130, 10);
            }

            if (PlayerThree != null)
            {
                _player3HUDAlive = new VisualObject2D();
                _player3HUDAlive.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\Il-Wrath");
                _player3HUDAlive.Position = new Vector2(10, GameScreen.ScreenDimensions.Y - 60);
                _player3HUDAlive.RotationAngle = 0;
                _player3HUDAlive.Origin = new Vector2(0, 0);
                _player3HUDAlive.Scale = 0.5f;

                p3HP = new Vector2(60, GameScreen.ScreenDimensions.Y - 60);
            }

            if (PlayerFour != null)
            {
                _player4HUDAlive = new VisualObject2D();
                _player4HUDAlive.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\Il-Wrath");
                _player4HUDAlive.Position = new Vector2(GameScreen.ScreenDimensions.X - 50, GameScreen.ScreenDimensions.Y - 60);
                _player4HUDAlive.RotationAngle = 0;
                _player4HUDAlive.Origin = new Vector2(0, 0);
                _player4HUDAlive.Scale = 0.5f;

                p4HP = new Vector2(GameScreen.ScreenDimensions.X - 130, GameScreen.ScreenDimensions.Y - 60);
            }
        }
        #endregion

        #region Mutators
        #region Player Properties
        // Mutators for the player stats
        public HumanPlayer PlayerOne
        {
            get { return _playerOneHUD; }
            set { _playerOneHUD = value; }
        }

        public HumanPlayer PlayerTwo
        {
            get { return _playerTwoHUD; }
            set { _playerTwoHUD = value; }
        }

        public HumanPlayer PlayerThree
        {
            get { return _playerThreeHUD; }
            set { _playerThreeHUD = value; }
        }

        public HumanPlayer PlayerFour
        {
            get { return _playerFourHUD; }
            set { _playerFourHUD = value; }
        }
        #endregion

        #region Other Properties
        public Screens.GameplayScreen GameScreen
        {
            get { return _gameScreen; }
            set { _gameScreen = value; }
        }

        #endregion
        #endregion

        #region Private Methods
        private string getPlayerHealthLeft(HumanPlayer thePlayer)
        {
            int x;
            string playerHealth = "";

            for (x = 0; x < thePlayer.PlayersShip.MaxHealth; ++x)
            {
                if (thePlayer.PlayersShip.Health > x)
                {
                    playerHealth += "|";
                }
                else
                {
                    playerHealth += " ";
                }
            }

            return playerHealth;
        }

        private string getPlayerHealthRight(HumanPlayer thePlayer)
        {
            int x;
            string playerHealth = "";

            for (x = 0; x < thePlayer.PlayersShip.MaxHealth; ++x)
            {
                if (thePlayer.PlayersShip.Health > x)
                {
                    playerHealth += "|";
                }
                else
                {
                    playerHealth += " ";
                }
            }

            return playerHealth;
        }
        #endregion

        #region Draw
        public void Draw()
        {
            _playerHP.Begin();

            if (_player1HUDAlive != null)
            {
                _player1HUDAlive.Draw();
                _playerHP.DrawString(font, "Shields: " + getPlayerHealthLeft(PlayerOne), p1HP, Color.Red);
            }

            if (_player2HUDAlive != null)
            {
                _player2HUDAlive.Draw();
                _playerHP.DrawString(font, "Shields: " +getPlayerHealthRight(PlayerTwo), p2HP, Color.Red);
            }

            if (_player3HUDAlive != null)
            {
                _player3HUDAlive.Draw();
                _playerHP.DrawString(font, "Shields: " + getPlayerHealthLeft(PlayerThree), p3HP, Color.Red);
            }

            if (_player4HUDAlive != null)
            {
                _player4HUDAlive.Draw();
                _playerHP.DrawString(font, "Shields: " + getPlayerHealthRight(PlayerFour), p4HP, Color.Red);
            }

            _playerHP.End();
        }
        #endregion
    }

}
