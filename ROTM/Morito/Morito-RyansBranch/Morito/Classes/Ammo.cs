using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Morito.Screens;
using Microsoft.Xna.Framework;

namespace Morito
{
    public abstract class Ammo : MortalPhysicalObject
    {
        #region Member Variables
            private WeaponWithAmmo _owner;
            private double _lifeTime;
            private double _spawnTime;

            #region InheritedClass Variables
            #endregion
        #endregion
        #region Properties
            public AttachedWeapon Owner
            {
                get { return _owner; }
            }
        #endregion

        #region Constructors
            public Ammo(GameScreen gameScreen, WeaponWithAmmo owner, float maxHealth, 
                        GameTime gameTime, double lifeTime)
                : base(gameScreen, maxHealth, 0d)
            {
                _owner = owner;
                _spawnTime = gameTime.TotalRealTime.TotalSeconds;
                _lifeTime = lifeTime;
            }
        #endregion

        #region Public Methods
            public override void Update(GameTime gameTime)
            {
                this.Move();

                this.UpdateMortality(gameTime);
                if (_lifeTime != -1 && gameTime.TotalRealTime.TotalSeconds > _spawnTime + _lifeTime)
                {
                    Health = 0;
                    Died = true;
                    _owner.FiredAmmo.Remove(this);
                }
            }

            public override void Draw()
            {
                if (this.IsAlive())
                    base.Draw();
            }
        #endregion
        #region Private Methods
        #endregion
        #region Abstract Methods
        #endregion
    }
}
