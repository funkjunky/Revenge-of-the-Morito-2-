#region Using Declaration
using Microsoft.Xna.Framework;
#endregion Using Declaration

namespace Morito
{
    public class Bullet : MortalPhysicalObject
    {
        #region Member Variables
        #endregion
        #region Properties
        public override string ClassName { get { return typeof(Bullet).FullName; } }
        #endregion
        #region Constructor
            public Bullet(Screens.GameScreen gameScreen)
                : base(gameScreen, 1f, 0d)
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
            public override void Update(GameTime gameTime)
            {
                Position2D -= Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                this.UpdateMortality(gameTime);
            }

            public override void Draw()
            {
                if (this.IsAlive())
                    base.Draw();
            }
        #endregion
    }
}
