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
        private List<Ammo> _firedAmmo = new List<Ammo>();

        #region InheritedClass Variables
        #endregion
        #endregion
        #region Properties
        public Ship Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        public List<Ammo> FiredAmmo
        {
            get { return _firedAmmo; }
        }
        #endregion

        #region Constructors
        public AttachedWeapon(GameplayScreen gameplayScreen, Ship owner)
            : base(gameplayScreen, 1f, 0d)
        {
            this._owner = owner;
        }
        #endregion

        #region Public Methods
        public override void Draw()
        {
            //base.Draw();

            foreach (Ammo ammo in _firedAmmo)
            {
                ammo.Draw();
            }
        }
        public override void Update(GameTime gameTime)
        {
            for (int i = 0; i < _firedAmmo.Count; ++i)
            {
                _firedAmmo.ElementAt(i).Update(gameTime);
            }
        }
        #endregion
        #region Private Methods
        #endregion
        #region Abstract Methods
        public abstract void Fire(float fireAngle, GameTime gameTime);
        #endregion
    }
}