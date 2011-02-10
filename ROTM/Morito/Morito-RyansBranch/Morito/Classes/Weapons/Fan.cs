using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Morito.Screens;
using Morito.Utilities;

namespace Morito
{
    public class Fan : AttachedWeapon
    {
        #region Constants
        private const float PUSH_POWER = 0.15f;
        private const float MAX_RANGE  = 60f;
        #endregion

        #region Constructors
            public Fan(GameScreen gameScreen, Ship owner)
            : base(gameScreen, owner, 1f, 0.1d)
            {

            }
        #endregion

        #region Public Methods
            public override bool Fire(float fireAngle, GameTime gameTime)
            {
                if (!canFire(gameTime))
                    return false;

                // Push objects in the direction of fire
                double dist;
                Vector2 dir;
                foreach (VisualObject3D obj in GameScreen.Level.Pieces)
                {
                    dir  = obj.Position2D - Owner.Position2D;
                    dist = Vector2Helper.FindDistanceOfVector(dir);
                    Angle angle = dir.getAngle();
                    if ( dist > MAX_RANGE || !angle.isAngleBetweenAngles(new Angle(fireAngle - Math.PI / 8), new Angle(fireAngle + Math.PI / 8)) )
                        continue;

                    if (obj is isPhysicsable)
                    {
                        dir.Normalize();
                        dir = dir.getDirectedVector(PUSH_POWER * (1.0f - (float)dist / MAX_RANGE) );
                        ((isPhysicsable)obj).Velocity += dir;
                    }
                }

                LastShot = gameTime.TotalRealTime.TotalSeconds;
                return true;
            }

            public override void Collide(isCollidable fellowCollider)
            {
            }
        #endregion
    }
}
