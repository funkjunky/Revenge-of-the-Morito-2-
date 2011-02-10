using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Morito.Screens;
using Morito.Utilities;
using Microsoft.Xna.Framework.Graphics;

namespace Morito
{
    public class TestGun : WeaponWithAmmo
    {
        #region Constructors
            public TestGun(GameScreen gameScreen, Ship owner)
            : base(gameScreen, owner, 1f, 0.5d, 0.1d, 5)
            {
                this.ObjectModel = gameScreen.ScreenManager.Game.Content.Load<Model>(@"Models\simpleWeapon");
            }
        #endregion

        #region Public Methods
            public override bool Fire(float fireAngle, GameTime gameTime)
            {
                if (!canFire(gameTime))
                    return false;

                launchAmmo(fireAngle, gameTime, new Bullet(GameScreen, this, gameTime, 1d));

                LastShot = gameTime.TotalRealTime.TotalSeconds;

                return true;
            }

            public override void Collide(isCollidable fellowCollider)
            {
            }
        #endregion
    }
}
