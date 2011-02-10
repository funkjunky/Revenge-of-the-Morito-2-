#region Using Declaration
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
#endregion Using Declaration


namespace Morito
{
    public class Ship : RespawnablePhysicalObject
    {
        #region Member Variables
            protected float _rotationSpeed = 0.1f;

            private AttachedWeapon _frontWeapon;
            private AttachedWeapon _backWeapon;
            private AttachedWeapon _rightWeapon;
            private AttachedWeapon _leftWeapon;
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
        public Ship(Screens.GameplayScreen gameplayScreen, Vector3 respawnPoint)
            : base(gameplayScreen, 10f, 0d, 8d, respawnPoint)
        {
            //The follow code is to test the weapons
            _frontWeapon = new TestGun(gameplayScreen, this);
            _rightWeapon = new TestGun(gameplayScreen, this);
        }
        #endregion

        #region isCollidable
            #region Public Methods
                public override void Collide(isCollidable fellowCollider)
                {
                    this.TakeDamage(MortalPhysicalObject.COLLISION_DAMAGE);

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
            this.Move();
            #region Bullet Update
                _frontWeapon.Update(gameTime);
                //_backWeapon.Update(gameTime);
                //_rightWeapon.Update(gameTime);
                //_leftWeapon.Update(gameTime);
            #endregion Bullet Update

            this.UpdateMortality(gameTime);
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

        public void FireWeapon(Keys key, GameTime gameTime)
        {
            float facing = RotationAngle;

            if (key == (Keys)HumanPlayer.KeyBoardControls.FireUp && _frontWeapon != null)
                _frontWeapon.Fire(facing, gameTime);

            if (key == (Keys)HumanPlayer.KeyBoardControls.FireDown && _backWeapon != null)
                _backWeapon.Fire(facing - (float)Math.PI, gameTime);

            if (key == (Keys)HumanPlayer.KeyBoardControls.FireLeft && _leftWeapon != null)
                _leftWeapon.Fire(facing + (float)Math.PI / 2, gameTime);

            if (key == (Keys)HumanPlayer.KeyBoardControls.FireRight && _rightWeapon != null)
                _rightWeapon.Fire(facing - (float)Math.PI / 2, gameTime);
        }
        #endregion
    }
}
