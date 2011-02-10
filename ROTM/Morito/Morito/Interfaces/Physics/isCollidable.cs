using Microsoft.Xna.Framework;

namespace Morito
{
    public interface isCollidable : isPhysicsable
    {
        BoundingSphere BSphere { get; set; }
        void Collide(isCollidable fellowCollider);
    }
}
