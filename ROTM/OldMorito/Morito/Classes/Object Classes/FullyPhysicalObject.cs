using Microsoft.Xna.Framework;

namespace Morito
{
    public abstract class FullyPhysicalObject : PhysicalObject, isCollidable
    {
        #region Member Variables
            protected BoundingSphere _bSphere;
        #endregion

        #region Properties
            public BoundingSphere BSphere
        {
            get { return _bSphere; }
            set { _bSphere = value; }
        }
        #endregion

        #region Constructors
        public FullyPhysicalObject(Screens.GameScreen gameScreen)
            : base(gameScreen)
        {

        }
        #endregion

        #region Complete Methods
        public override void Move()
        {
            base.Move();
            BoundingSphere bSphere = BSphere;
            bSphere.Center = new Vector3(Position2D, 0.0f); //maybe try -0.67 if 0 doesn't work.
            BSphere = bSphere;
            //if the radiuses change we may be in trouble... 
            //I suppose whatever changes the radius may be responsible for changing the bounding sphere radius, 
            //ensuring that the object is collidable so the object has the property.
        }
        #endregion

        #region Abstract Methods
            public abstract void Collide(isCollidable fellowCollider);
        #endregion
    }
}
