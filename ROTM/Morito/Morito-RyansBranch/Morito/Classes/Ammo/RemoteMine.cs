using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Morito.Screens;
using Microsoft.Xna.Framework;

namespace Morito
{
    public class RemoteMine : Ammo
    {
        #region Member Fields
        #endregion
        #region Properties
        #endregion

        #region Constructors
        public RemoteMine(GameScreen gameScreen, WeaponWithAmmo owner, GameTime gametime, double lifeTime)
            : base(gameScreen, owner, 1f, gametime, lifeTime)
        {

        }
        #endregion

        #region Methods
        public override void Collide(isCollidable fellowCollider)
        {
            this.TakeDamage(MortalPhysicalObject.COLLISION_DAMAGE);
        }
        #endregion
    }
}
