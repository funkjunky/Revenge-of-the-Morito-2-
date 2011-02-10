#region Using Declaration
using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

using Morito.Utilities;
#endregion Using Declaration


namespace Morito
{
    public class Asteroid : RespawnablePhysicalObject
    {
        static Random rand = new Random();

        #region Member Variables
            protected Vector3 _rotationVelocity;     //The rotation speed of the asteroid
        #endregion
        #region Properties
            public override string ClassName { get { return typeof(Asteroid).FullName; } }
            public Vector3 RotationVelocity
            {
                get { return _rotationVelocity; }
                set { _rotationVelocity = value; }
            }
        #endregion
        #region Constructors
            private Asteroid()
                : base(null, 0f, 0d, 0d, Vector3.Zero)
            {
                AllowFriction = false;
            }

            public Asteroid(Screens.GameScreen gameScreen)
                : base(gameScreen, 10f, 0d, 5d, new Vector3(MakeRespawnPoint(), 0.67f))
            {
                AllowFriction = false;
            }
            public Asteroid(Screens.GameScreen gameScreen, Camera camera1)
                : base(gameScreen, 10f, 0d, 5d, new Vector3(MakeRespawnPoint(), 0.67f))
            {
                AllowFriction = false;
                ReferenceCamera = camera1;
            }
        #endregion


        #region isCollidable
            #region Public Methods
                public override void Collide(isCollidable fellowCollider)
                {
                    this.TakeDamage(MortalPhysicalObject.COLLISION_DAMAGE);

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
        

        #region Private Methods
            protected void Rotate()
            {
                Rotation += RotationVelocity;
            }
        #endregion
        #region Public Methods
            public override Vector2 CreateRespawnPoint()
            {
                bool onSide = rand.Next(1) == 0;
                Vector2 v2;

                if (onSide)
                    v2 = rand.nextVector2Interval(new Vector2(-80, -60), new Vector2(-70, 60));
                else
                    v2 = rand.nextVector2Interval(new Vector2(-60, -80), new Vector2(-60, -70));

                return v2;
            }
            public static Vector2 MakeRespawnPoint()
            {
                return (new Asteroid()).CreateRespawnPoint();
            }

            public override void Update(GameTime gameTime)
            {
                this.Move();
                this.Rotate();

                this.UpdateMortality(gameTime); 

                base.Update(gameTime);
            }
            public override void Draw()
            {
                //Draws the asteroid if it's not destroyed
                if (this.IsAlive())
                    base.Draw();
            }

            public override void LoadFromXElement(XElement root)
            {
                base.LoadFromXElement(root);

                RotationVelocity = new Vector3();
                RotationVelocity = RotationVelocity.loadFromXElement(root.Element("RotationVelocity").Element("Vector3"));
            }

            public override XElement serializeToXElement()
            {
                XElement root = base.serializeToXElement();
                root.Add(new XElement("RotationVelocity", RotationVelocity.serializeToXElement()));

                return root;
            }
        #endregion
    }
}
