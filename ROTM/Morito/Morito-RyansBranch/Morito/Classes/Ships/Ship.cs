#region Using Declaration

using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion Using Declaration


namespace Morito
{
    public enum FireDirection
    {
        Up, Right, Down, Left
    }
    public class Ship : RespawnablePhysicalObject
    {
        #region Member Variables
            private Player _owner;


            protected float _rotationSpeed = 0.1f;

            private AttachedWeapon _frontWeapon;
            private AttachedWeapon _backWeapon;
            private AttachedWeapon _rightWeapon;
            private AttachedWeapon _leftWeapon;

            private Screens.GameplayScreen gameplayScreen;
        #endregion
        #region Properties
            public float RotationSpeed
            {
                get { return _rotationSpeed; }
                set { _rotationSpeed = value; }
            }

            public float RotationAngle
            {
                get { return _rotation.Z; }
                set { _rotation.Z = value; }
            }
        #endregion properties
        #region Constructors
        public Ship(Screens.GameScreen gameScreen, Vector3 respawnPoint, Screens.GameplayScreen gpScreen, Player player)
            : base(gameScreen, 10f, 0d, 8d, respawnPoint)
        {
            // ship should know who owns it
            _owner = player;
            gameplayScreen = gpScreen;

            //The follow code is to test the weapons
            _frontWeapon = new Fan(gameScreen, this);
            _rightWeapon = new RemoteMineLauncher(gameScreen, this);
			_leftWeapon = new TestGun(gameScreen, this);
            _backWeapon = new TestGun(gameScreen, this);
        }
        #endregion

        #region isCollidable
            #region Public Methods
                public override void Collide(isCollidable fellowCollider)
                {
                    if (fellowCollider.GetType() == typeof(Morito.Bullet))
                        _owner.HitsTaken++;
                    else if (fellowCollider.GetType() == typeof(Morito.Asteroid))
                        _owner.AsteriodsHit++;

                    this.TakeDamage(MortalPhysicalObject.COLLISION_DAMAGE);
                    gameplayScreen.CombatTextList.Add(new CombatText(false, COLLISION_DAMAGE, gameplayScreen.Time, this.Position, gameplayScreen, gameplayScreen.Camera1));

                    SoundEngine sfxE = MoritoFighterGame.MoritoFighterGameInstance.SfxE;
                    sfxE.trackCue = sfxE.soundBank.GetCue("impact");
                    sfxE.trackCue.Play();
                    //or sfxE.soundBank.PlayCue("impact"); if you don't care about control.
                    /*
                    collision.Play();
                    float bothMass = Mass + fellowCollider.Mass;

                    TakeDamage((fellowCollider.Mass - Mass) * (_velocity.Length() - fellowCollider.Velocity.Length()), gameTime.TotalGameTime.TotalSeconds);

                    if (Mass == fellowCollider.Mass)
                        _velocity = fellowCollider.Velocity;
                    else
                    {
                        _velocity.X = ((fellowCollider.Mass - Mass) / bothMass * _velocity.X)
                                      + ((Mass * 2) / bothMass * fellowCollider.Velocity.X);

                        _velocity.Y = ((fellowCollider.Mass - Mass) / bothMass * _velocity.Y)
                                      + ((Mass * 2) / bothMass * fellowCollider.Velocity.Y);
                    }
                    */
                }          
            #endregion
        #endregion


        #region Public Methods
        public override void Update(GameTime gameTime)
        {
            #region Bullet Update
            if (_frontWeapon != null)
                _frontWeapon.Update(gameTime);
            if (_backWeapon != null)
                _backWeapon.Update(gameTime);
            if (_rightWeapon != null)
                _rightWeapon.Update(gameTime);
            if (_leftWeapon != null)
                _leftWeapon.Update(gameTime);
            #endregion Bullet Update

            this.UpdateMortality(gameTime);

            base.Update(gameTime);
        }

        public override void Draw()
        {
            if (this.IsAlive())
                base.Draw();

            MoritoFighterGame.MoritoFighterGameInstance.SpriteBatchDrawable.Begin();

            if (_frontWeapon != null)
                _frontWeapon.Draw();

            if (_backWeapon != null)
                _backWeapon.Draw();

            if (_rightWeapon != null)
                _rightWeapon.Draw();

            if (_leftWeapon != null)
                _leftWeapon.Draw();

            MoritoFighterGame.MoritoFighterGameInstance.SpriteBatchDrawable.End();
        }


        public override Vector2 CreateRespawnPoint()
        {
            return new Vector2(RespawnPoint.X, RespawnPoint.Y);
        }

        public void FireWeapon(FireDirection dir, GameTime gameTime)
        {
            float facing = RotationAngle;

            if (dir == FireDirection.Up && _frontWeapon != null)
            {
                if (_frontWeapon.Fire(facing, gameTime))
                    _owner.ShotsFired++;
            }
            else if (dir == FireDirection.Down && _backWeapon != null)
            {
                if (_backWeapon.Fire(facing - (float)Math.PI, gameTime))
                    _owner.ShotsFired++;
            }
            else if (dir == FireDirection.Left && _leftWeapon != null)
            {
                if (_leftWeapon.Fire(facing + (float)Math.PI / 2, gameTime))
                    _owner.ShotsFired++;
            }
            else if (dir == FireDirection.Right && _rightWeapon != null)
            {
                if (_rightWeapon.Fire(facing - (float)Math.PI / 2, gameTime))
                    _owner.ShotsFired++;
            }
        }
        #endregion
    }
}
