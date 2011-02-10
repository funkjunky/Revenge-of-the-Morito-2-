#region Using Declaration
using Microsoft.Xna.Framework;
#endregion Using Declaration

namespace Morito
{
    public class Player
    {
        Ship _playerShip;
        PlayerIndex _playersIndex;

        public Player(Screens.GameplayScreen gameplayScreen, Vector3 respawnPoint)
        {
            if(this.PlayersShip == null)
                this.PlayersShip = new Ship(gameplayScreen, respawnPoint);

            //if(this._playersIndex == null)
            //    this._playersIndex = PlayerIndex. next available index. (perhaps, somehow);
        }

        public PlayerIndex PlayersIndex{
            get { return this._playersIndex; }
            set{ this._playersIndex = value;}
        }

        public Ship PlayersShip
        {
            get { return this._playerShip; }
            set { this._playerShip = value; }
        }

        public virtual void Update(GameTime gameTime)
        {
            PlayersShip.Update(gameTime);
        }

        public virtual void Draw()
        {
            PlayersShip.Draw();
        }
    }
}
