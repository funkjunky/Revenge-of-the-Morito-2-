using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Morito
{
    public class HUD
    {
        #region Member Variables
        // HUD variables
        Dictionary<string, string> hudMessages;
        private HumanPlayer _playerOneStats, _playerTwoStats, _playerThreeStats, _playerFourStats;
        private VisualObject2D _player1HUDAlive, _player2HUDAlive, _player3HUDAlive, _player4HUDAlive;
        private VisualObject2D _player1HUDDead, _player2HUDDead, _player3HUDDead, _player4HUDDead;
        private Screens.GameplayScreen _gameScreen;
        #endregion

        #region Constructor and Initilizer
        public HUD()
        {
            hudMessages = MoritoFighterGame.MoritoFighterGameInstance._screenManager.DisplayedMessages;
        }

        public void initilizeHUD(Screens.GameplayScreen screen)
        {
            _gameScreen = screen;

            if (PlayerOne != null)
            {
                _player1HUDAlive = new VisualObject2D();
                _player1HUDAlive.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\leftHUD_Alive");
                _player1HUDAlive.Position = new Vector2(10, 10);
                _player1HUDAlive.RotationAngle = 0;
                _player1HUDAlive.Origin = new Vector2(0,0);
                _player1HUDAlive.Scale = 1.0f;

                _player1HUDDead = new VisualObject2D();
                _player1HUDDead.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\leftHUD_Dead");
                _player1HUDDead.Position = new Vector2(10, 10);
                _player1HUDDead.RotationAngle = 0;
                _player1HUDDead.Origin = new Vector2(0, 0);
                _player1HUDDead.Scale = 1.0f;
            }

            if (PlayerTwo != null)
            {
                _player2HUDAlive = new VisualObject2D();
                _player2HUDAlive.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\rightHUD_Alive");
                _player2HUDAlive.Position = new Vector2(GameScreen.ScreenDimensions.X-135, 10);
                _player2HUDAlive.RotationAngle = 0;
                _player2HUDAlive.Origin = new Vector2(0, 0);
                _player2HUDAlive.Scale = 1.0f;

                _player2HUDDead = new VisualObject2D();
                _player2HUDDead.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\rightHUD_Dead");
                _player2HUDDead.Position = new Vector2(GameScreen.ScreenDimensions.X - 135, 10);
                _player2HUDDead.RotationAngle = 0;
                _player2HUDDead.Origin = new Vector2(0, 0);
                _player2HUDDead.Scale = 1.0f;
            }

            if (PlayerThree != null)
            {
                _player3HUDAlive = new VisualObject2D();
                _player3HUDAlive.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\leftHUD_Alive");
                _player3HUDAlive.Position = new Vector2(10, GameScreen.ScreenDimensions.Y - 60);
                _player3HUDAlive.RotationAngle = 0;
                _player3HUDAlive.Origin = new Vector2(0, 0);
                _player3HUDAlive.Scale = 1.0f;

                _player3HUDDead = new VisualObject2D();
                _player3HUDDead.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\leftHUD_Dead");
                _player3HUDDead.Position = new Vector2(10, GameScreen.ScreenDimensions.Y - 60);
                _player3HUDDead.RotationAngle = 0;
                _player3HUDDead.Origin = new Vector2(0, 0);
                _player3HUDDead.Scale = 1.0f;
            }

            if (PlayerFour != null)
            {
                _player4HUDAlive = new VisualObject2D();
                _player4HUDAlive.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\rightHUD_Alive");
                _player4HUDAlive.Position = new Vector2(GameScreen.ScreenDimensions.X - 135, GameScreen.ScreenDimensions.Y - 60);
                _player4HUDAlive.RotationAngle = 0;
                _player4HUDAlive.Origin = new Vector2(0, 0);
                _player4HUDAlive.Scale = 1.0f;

                _player4HUDDead = new VisualObject2D();
                _player4HUDDead.Texture = GameScreen.ScreenManager.Game.Content.Load<Texture2D>("Textures\\rightHUD_Dead");
                _player4HUDDead.Position = new Vector2(GameScreen.ScreenDimensions.X - 135, GameScreen.ScreenDimensions.Y - 60);
                _player4HUDDead.RotationAngle = 0;
                _player4HUDDead.Origin = new Vector2(0, 0);
                _player4HUDDead.Scale = 1.0f;
            }
        }
        #endregion

        #region Mutators
        #region Player proporties
        // Mutators for the player stats
        public HumanPlayer PlayerOne
        {
            get { return _playerOneStats; }
            set { _playerOneStats = value; }
        }

        public HumanPlayer PlayerTwo
        {
            get { return _playerTwoStats; }
            set { _playerTwoStats = value; }
        }

        public HumanPlayer PlayerThree
        {
            get { return _playerThreeStats; }
            set { _playerThreeStats = value; }
        }

        public HumanPlayer PlayerFour
        {
            get { return _playerFourStats; }
            set { _playerFourStats = value; }
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

        #region Draw
        public void Draw()
        {
            if (_player1HUDAlive != null)
                if (PlayerOne.PlayersShip.IsAlive())
                {
                    _player1HUDAlive.Draw();
                }
                else
                {
                    _player1HUDDead.Draw();
                }
            if (_player2HUDAlive != null)
                if (PlayerTwo.PlayersShip.IsAlive())
                {
                    _player2HUDAlive.Draw();
                }
                else
                {
                    _player2HUDDead.Draw();
                }
            if (_player3HUDAlive != null)
                if (PlayerThree.PlayersShip.IsAlive())
                {
                    _player3HUDAlive.Draw();
                }
                else
                {
                    _player2HUDDead.Draw();
                }
            if (_player4HUDAlive != null)
                if (PlayerFour.PlayersShip.IsAlive())
                {
                    _player4HUDAlive.Draw();
                }
                else
                {
                    _player4HUDDead.Draw();
                }
        }
        #endregion
    }

}
