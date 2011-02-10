using Microsoft.Xna.Framework;

namespace Morito
{
    public interface isPhysicsable
    {
        Vector2 Force
        {
            get;
            set;
        }

        Vector2 Velocity
        {
            get;
            set;
        }

        float Mass
        {
            get;
            set;
        }

        Vector2 Position2D
        { 
            get; 
            set; 
        }

        float KineticFriction
        {
            get;
            //set; //I'll put this in at some point... right now its easier if it isn't in.
        }
    }
}
