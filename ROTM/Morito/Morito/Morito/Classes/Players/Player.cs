#region Using Declaration
using Microsoft.Xna.Framework;
#endregion Using Declaration

namespace Morito
{
    public class Player : isStatistical
    {
        #region various variables
        // Statistical Data
        private int _kills = 0;
        private int _deaths = 0;
        private int _points = 0;
        private int _shotsFired = 0;
        private int _shotsHit = 0;
        private int _hitsTaken = 0;
        private int _asteriodsHit = 0;

        Ship _playerShip;
        PlayerIndex _playersIndex;
        #endregion

        #region Mutators
        public int Kills
        {
            get { return _kills; }
            set { _kills += value; }
        }

        public int Deaths
        {
            get { return _deaths; }
            set { _deaths += value; }
        }

        public int Points
        {
            get { return _points; }
            set { _points += value; }
        }

        public int ShotsFired
        {
            get { return _shotsFired; }
            set { _shotsFired++; }
        }

        public int ShotsHit
        {
            get { return _shotsHit; }
            set { _shotsHit++; }
        }

        public int HitsTaken
        {
            get { return _hitsTaken; }
            set { _hitsTaken++; }
        }

        public int AsteriodsHit
        {
            get { return _asteriodsHit; }
            set { _asteriodsHit++; }
        }

        public PlayerIndex PlayersIndex
        {
            get { return this._playersIndex; }
            set { this._playersIndex = value; }
        }

        public Ship PlayersShip
        {
            get { return this._playerShip; }
            set { this._playerShip = value; }
        }
        #endregion

        #region Public Methods
        public Player(Screens.GameplayScreen gameplayScreen, Vector3 respawnPoint)
        {
            if (this.PlayersShip == null)
                this.PlayersShip = new Ship(gameplayScreen, respawnPoint, gameplayScreen, this);

            //if(this._playersIndex == null)
            //    this._playersIndex = PlayerIndex. next available index. (perhaps, somehow);
        }
        #endregion

        #region Update and Draw
        public virtual void Update(GameTime gameTime)
        {
            PlayersShip.Update(gameTime);
        }

        public virtual void Draw()
        {
            PlayersShip.Draw();
        }
        #endregion
    }
}
