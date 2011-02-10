using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Morito.Screens;
using Morito.Utilities;

namespace Morito
{
    
    public abstract class WeaponWithAmmo : AttachedWeapon
    {
        #region Member Variables
            private List<Ammo> _firedAmmo = new List<Ammo>();

            private double _previousShot = 0;
            private double _fireVelocity;
            private int _maxAmmo;
        #endregion
        #region Properties
        public List<Ammo> FiredAmmo
        {
            get { return _firedAmmo; }
        }
        #endregion

        #region Constructors
        public WeaponWithAmmo(GameScreen gameScreen, Ship owner, float maxHealth, double fireRate, 
                              double fireVelocity, int maxAmmo)
            :base(gameScreen, owner, maxHealth, fireRate)
        {
            _fireVelocity = fireVelocity;
            _maxAmmo = maxAmmo;
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

        #region Protected Methods
        protected void launchAmmo(float fireAngle, GameTime gameTime, Ammo ammo)
        {
            if (_firedAmmo.Count >= _maxAmmo 
                || gameTime.TotalRealTime.TotalSeconds < _previousShot + FireRate)
                return;

            _previousShot = gameTime.TotalRealTime.TotalSeconds;

            Vector2 fireVector = Vector2Helper.FindVectorToAngle(fireAngle);

            Vector2 bulletPosition = new Vector2(Owner.Position.X + (fireVector.X * 6f),
                                                 Owner.Position.Y + (fireVector.Y * 6f));

            ammo.Position2D = bulletPosition;
            ammo.Velocity = fireVector * (float)(1 + _fireVelocity);
            ammo.ReferenceCamera = GameScreen.Camera1;
            ammo.ObjectModel = GameScreen.BulletModel;


            FiredAmmo.Add(ammo);
            GameScreen.Collisions.Collidables.Add(ammo);
            GameScreen.ObjectsToBeWrapped.Add(ammo);
            GameScreen.Level.Pieces.Add(ammo);
        }
        #endregion
    }
}
