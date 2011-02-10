using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Morito
{
    public abstract class PhysicalObject : VisualObject3D, isPhysicsable
    {
        #region Member Variables
            private Vector2 _velocity;                           //should be copied to the movement interface
            private float _mass = 1.0f;
            private Vector2 _force;

            protected bool _allowFriction = true;
        #endregion

        #region Properties
            public float Mass
            {
                get { return _mass; }
                set { _mass = value; }
            }

            public Vector2 Velocity
            {
                get { return _velocity; }
                set { _velocity = value; }
            }

            public Vector2 Force
            {
                get { return _force; }
                set { _force = value; }
            }

            public bool AllowFriction
            {
                get { return _allowFriction; }
                set { _allowFriction = value; }
            }

            public virtual float KineticFriction
            {
                get { return PhysicalObject.KineticFrictionDefault(); }
            }
        #endregion

        #region Constructors
            public PhysicalObject(Screens.GameScreen gameScreen)
                : base(gameScreen)
            {

            }
        #endregion

        #region Complete Methods
            public override void Update(GameTime gameTime)
            {
                Move();

                base.Update(gameTime);
            }

            public static float KineticFrictionDefault()
            {
                return 0.01f;
            }

            //so like, light speed. Any object wanting to set a max speed, will need to do so on their own. Bah this isn't a default...
            public static float GlobalMaxSpeed()
            {
                return float.MaxValue;
            }


            public virtual void Move()
            {
                //kinetic friction is not implemented exactly like kinetic friction... it should.
                //Maybe it is implemented... if theirs some crazy way to think of it, make a conversion function.
                //I'm going to apply physics peice by peice. Starting with regular force then kinetic friction.

                //apply acceleration... well once i and mass in here. TODO: add mass =P
                Velocity += Force;

                //friction
                ApplyFriction();

                //Stops the asteroid from going too fast
                if (Velocity.Length() > GlobalMaxSpeed())
                {
                    Velocity.Normalize();
                    Velocity *= GlobalMaxSpeed();
                }

                //Just makes the asteroid move
                Position2D += Velocity;


                //put this in the world object that handles physics and collision detection.
                //Wrapping code
                //if (v2dPosition.X + v2dVelocity.X > (GameScreen.X))
                //    v2dPosition.X = 0.0f;

                //if (v2dPosition.X + v2dVelocity.X < 0)
                //    v2dPosition.X = (GameScreen.X);

                //if (v2dPosition.Y + v2dVelocity.Y > (GameScreen.Y))
                //    v2dPosition.Y = 0.0f;

                //if (v2dPosition.Y + v2dVelocity.Y < 0)
                //    v2dPosition.Y = (GameScreen.Y);
            }


            public void ApplyFriction()
            {
                if (!AllowFriction)
                    return;

                /*
                //store the sign of each coordinate
                bool isPositiveX = isphysics.Velocity.X > 0;
                bool isPositiveY = isphysics.Velocity.Y > 0;

                Vector2 thisVector = isphysics.Velocity;

                //reduce X and Y
                thisVector.X = Math.Abs(thisVector.X) - isphysics.KineticFriction;
                thisVector.Y = Math.Abs(thisVector.Y) - isphysics.KineticFriction;
            
                if (!isPositiveX) thisVector.X *= -1;
                if (!isPositiveY) thisVector.Y *= -1;

                //return true if the sign has changed.
                if ((isPositiveX && thisVector.X < 0) 
                        || (isPositiveY && thisVector.Y < 0) 
                        || (!isPositiveX && thisVector.X > 0) 
                        || (!isPositiveY && thisVector.Y > 0))
                    thisVector = Vector2.Zero;

                isphysics.Velocity = thisVector; 
                */
                Velocity *= (1f - KineticFriction);
                if (Velocity.Length() < 0.01f)
                    Velocity = Vector2.Zero;
            }

            public override void LoadFromXElement(XElement root)
            {
                base.LoadFromXElement(root);

                Mass =  Convert.ToSingle(root.Element("Mass").Value);
                Velocity = new Vector2();
                XElement b = root.Element("Velocity");
                XElement a = root.Element("Velocity").Element("Vector2");
                Velocity = Velocity.loadFromXElement(root.Element("Velocity").Element("Vector2"));
                Force = new Vector2();
                Force = Force.loadFromXElement(root.Element("Force").Element("Vector2"));
                AllowFriction = Convert.ToBoolean(root.Element("AllowFriction").Value);
            }

            public override XElement serializeToXElement()
            {
                XElement root = base.serializeToXElement();
                root.Add(new XElement("Mass", Mass));
                root.Add(new XElement("Velocity", Velocity.serializeToXElement()));
                root.Add(new XElement("Force", Force.serializeToXElement()));
                root.Add(new XElement("AllowFriction", AllowFriction));

                return root;
            }
        #endregion

        #region Abstract Methods
        #endregion
    }
}
