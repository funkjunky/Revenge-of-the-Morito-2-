#region Using Declaration
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
#endregion Using Declaration

namespace Morito
{
    public class Bullet : Ammo
    {
        #region Member Variables
        #endregion
        #region Properties
        public override float KineticFriction
        {
            get { return 0.00001f; }
        }
        #endregion
        #region Constructor
        public Bullet(Screens.GameScreen gameScreen, WeaponWithAmmo owner, GameTime spawnTime, double lifeTime)
                : base(gameScreen, owner, 1f, spawnTime, lifeTime)
            {

            }
        #endregion

        #region isCollidable
            #region Public Methods
                public override void Collide(isCollidable fellowCollider)
                {
                    this.TakeDamage(MortalPhysicalObject.COLLISION_DAMAGE);
                }
            #endregion
        #endregion
        
        #region Public Methods
            
        #endregion
    }
}
