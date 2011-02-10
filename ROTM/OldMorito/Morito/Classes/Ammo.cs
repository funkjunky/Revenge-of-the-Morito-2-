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
            private AttachedWeapon _owner;
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
            public Ammo(GameScreen gameScreen, AttachedWeapon owner, float maxHealth, 
                        double spawnTime, double lifeTime)
                : base(gameScreen, maxHealth, 0d)
            {
                _owner = owner;
                _spawnTime = spawnTime;
                _lifeTime = lifeTime;
            }
        #endregion

        #region Public Methods
            public override void Update(GameTime gameTime)
            {
                this.Move();

                this.UpdateMortality(gameTime);
                if (gameTime.TotalRealTime.TotalSeconds > _spawnTime + _lifeTime)
                {
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
