using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Morito.Screens;
using Morito.Utilities;

namespace Morito
{
    public class RemoteMineLauncher : WeaponWithAmmo
    {
        #region Constants
        private const float EXPLOSION_RADIUS = 5;
        private const float MAX_DAMAGE       = 5;
        #endregion

        #region Constructors
        public RemoteMineLauncher(GameScreen gameScreen, Ship owner)
            :base(gameScreen, owner, 1f, 2f, 0.1f, 1)
        {

        }
        #endregion

        #region Public Methods
        public override bool Fire(float fireAngle, GameTime gameTime)
        {
            if ( !canFire(gameTime) )
                return false;

            if (FiredAmmo.Count > 0)
            {
                //Explaoded!
                FiredAmmo[0].Health = 0;
                FiredAmmo[0].Died = true;
                FiredAmmo.Remove(FiredAmmo[0]);
                double dist;
                Vector2 dir;
                foreach (VisualObject3D obj in GameScreen.Level.Pieces)
                {
                    dist = Vector2Helper.FindDistanceOfVector(obj.Position2D - Owner.Position2D);
                    if ( dist >= EXPLOSION_RADIUS )
                        continue;

                    if (obj is isCollidable && obj is isPhysicsable)
                    {
                        dir = (obj.Position2D - Owner.Position2D);
                        dir.Normalize();
                        dir.getDirectedVector((float)(Math.Abs(dist / (EXPLOSION_RADIUS * 10))));
                        ((isPhysicsable)obj).Velocity += dir;
                    }

                    if (obj is isMortal)
                        ((isMortal)obj).TakeDamage((float)(Math.Abs(dist) - EXPLOSION_RADIUS));
                }

                LastShot = gameTime.TotalRealTime.TotalSeconds - FireRate + 0.1;
            }
            else
            {
                //Fiiiirrrreee!
                launchAmmo(fireAngle, gameTime, new RemoteMine(GameScreen, this, gameTime, -1d));
                LastShot = gameTime.TotalRealTime.TotalSeconds;
            }

            return true;
        }

        public override void Collide(isCollidable fellowCollider)
        {
        }
        #endregion
    }
}
