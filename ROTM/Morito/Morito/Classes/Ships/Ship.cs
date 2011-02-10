#region Using Declaration
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion Using Declaration


namespace Morito
{
    public class Ship : RespawnablePhysicalObject
    {
        #region Member Variables
            private Texture2D _txBulletTexture;                   //this should be a part of bullet. Hmmm pending further thought
            private List<Bullet> _bulletList = new List<Bullet>();   //really holds the bullets    
            protected float _rotationSpeed = 0.1f;
        #endregion
        #region Properties
            public override string ClassName { get { return typeof(Ship).FullName; } }
        public Texture2D BulletTexture
        {
            get { return _txBulletTexture; }
            set { _txBulletTexture = value; }
        }

        
        public List<Bullet> BulletList
        {
            get { return _bulletList; }
            set { _bulletList = value; }
        }

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
        public Ship(Screens.GameScreen gameScreen, Vector3 respawnPoint)
            : base(gameScreen, 10f, 0d, 8d, respawnPoint)
        {

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
        public override Vector2 CreateRespawnPoint()
        {
            return new Vector2(RespawnPoint.X, RespawnPoint.Y);
        }

        public void ShootBullet()
        {
            Vector2 bulletPosition = new Vector2(this.Position.X, this.Position.Y);

            Bullet newBullet = new Bullet(GameScreen)
                    {   
                        Position2D = bulletPosition, 
                        ObjectModel = MoritoFighterGame.MoritoFighterGameInstance.BulletModel
                    };
            this.BulletList.Add(newBullet);
        }

        public override void Update(GameTime gameTime)
        {
            this.Move();
            #region Bullet Update
            foreach (Bullet ShipBullet in _bulletList)
            {
                ShipBullet.Update(gameTime);
            }
            #endregion Bullet Update

            this.UpdateMortality(gameTime);
        }

        public override void Draw()
        {
            if (this.IsAlive())
                base.Draw();

            MoritoFighterGame.MoritoFighterGameInstance.SpriteBatchDrawable.Begin();
            foreach (Bullet shipBullet in _bulletList)
                shipBullet.Draw();
            MoritoFighterGame.MoritoFighterGameInstance.SpriteBatchDrawable.End();
        }
        #endregion
    }
}
