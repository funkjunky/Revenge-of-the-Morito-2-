using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Morito.Screens;
using Microsoft.Xna.Framework;

namespace Morito
{
    public abstract class AttachedWeapon : MortalPhysicalObject
    {
        #region Member Variables
        private Ship _owner;
        private double _fireRate;
        private double _lastShot;
        #endregion
        #region Properties
        public Ship Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public double FireRate
        {
            get { return _fireRate; }
        }

        public double LastShot
        {
            get { return _lastShot; }
            set { _lastShot = value; }
        }
        #endregion

        #region Constructors
        public AttachedWeapon(GameScreen gameScreen, Ship owner, float maxHealth, double fireRate)
            : base(gameScreen, maxHealth, 0d)
        {
            this._owner = owner;
            this._fireRate = fireRate;
            this._lastShot = 0;
        }
        #endregion

        #region Public Methods
        public override void Draw()
        {
            //base.Draw();
        }
        #endregion
        #region Private Methods
        protected bool canFire(GameTime gameTime)
        {
            return _lastShot + _fireRate < gameTime.TotalRealTime.TotalSeconds;
        }  
        #endregion
        #region Abstract Methods
        public abstract bool Fire(float fireAngle, GameTime gameTime);
        #endregion
    }
}