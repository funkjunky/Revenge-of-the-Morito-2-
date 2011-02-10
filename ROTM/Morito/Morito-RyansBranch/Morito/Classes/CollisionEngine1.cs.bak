using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Morito
{
    public class CollisionEngine
    {
        Dictionary<string, string> globalMessages;
        private List<isCollidable> _collidables;

        public List<isCollidable> Collidables
        {
            get { return _collidables; }
            set { _collidables = value; }
        }


        public CollisionEngine() { _collidables = new List<isCollidable>(); globalMessages = MoritoFighterGame.MoritoFighterGameInstance._screenManager.DisplayedMessages; }


        public void update()
        {
            globalMessages["whereCollision"] = "Collisions: (ie. [A-><-B @ (centreA)\n";
            for (int x = 0; x < _collidables.Count; x++)
            {
                for (int y = 0; y < _collidables.Count; y++)
                {
                    if (_collidables[x] != _collidables[y])
                    {
                        if (haveCollided(_collidables[x], _collidables[y]))
                        {
                            globalMessages["whereCollision"] += "[" + x + "-><-" + y + " @ "
                                                                + _collidables[x].BSphere.Center+"]";
                            //System.Console.WriteLine(x + ", " + y + ": objects have collided!\n");
                            //resolveCollision(ref _collidables[x], ref _collidables[y]);
                            this.resolveCollision(x, y);
                            _collidables[x].Collide(_collidables[y]);
                            _collidables[y].Collide(_collidables[x]);
                        }
                    }
                }
            }
        }

        
        public static bool haveCollided(isCollidable A, isCollidable B)
        {
            if (A is isMortal)
                if (((isMortal)A).IsDead())
                    return false;

            if (B is isMortal)
                if (((isMortal)B).IsDead())
                    return false;

            return A.BSphere.Intersects(B.BSphere);
        }


        public void resolveCollision(int i, int j)
        {
            //see: http://www.plasmaphysics.org.uk/collision2d.htm
            //float aRelVelocity = Collidables[a].Velocity - Collidables[b].Velocity;
            //float bRelVelocity = Collidables[b].Velocity - Collidables[b].Velocity; //Zero

            //Note: a related to 1 in the formulas and 2 relates to b. 
            //Also I used theta rather than alpha, 'cause I like theta better.

            //first set a bunch of variables for ease of use... 
            //this is too complicated for me to not have short variable names.
            float ma = Collidables[i].Mass;
            float mb = Collidables[j].Mass;

            float vxa = Collidables[i].Velocity.X;
            float vya = Collidables[i].Velocity.Y;
            float vxb = Collidables[j].Velocity.X;
            float vyb = Collidables[j].Velocity.Y;

            float xa = Collidables[i].Position2D.X;
            float ya = Collidables[i].Position2D.Y;
            float xb = Collidables[j].Position2D.X;
            float yb = Collidables[j].Position2D.Y;

            //calculates the angle of incident or whatever.
            double theta = Math.Atan((ya-yb)/(xa-xb));  //collision angle? (angle between the origins.)

            //calculate impact angle.
            double yv = Math.Atan((vya-vyb)/(vxa-vxb));  //impact angle (velocity angle)

            //No idea what this is...
            //a = tan(θ) = tan(γv+α)
            double a = Math.Tan(yv + theta);
            //nor this...
            //Δvx,2' = 2[ vx,1 - vx,2 + a.(vy,1 - vy,2 ) ] / [(1+a2).(1+m2 /m1 )]
            double dvxbp = 2 * (vxa - vxb + a * (vya - vyb)) / ((1 + a * a) * (1 + mb / ma)); 

            //calculate all the final velocities
            double vxaf = vxb + dvxbp;
            double vyaf = vyb + a*dvxbp;
            double vxbf = vxa - (mb/ma)*dvxbp;
            double vybf = vya - a*(mb/ma)*dvxbp;

            isPhysicsable AA = Collidables[i];
            isPhysicsable BB = Collidables[j];

            //apply final velocities to objects.
            AA.Velocity = new Vector2((float)vxaf, (float)vyaf);
            BB.Velocity = new Vector2((float)vxbf, (float)vybf);
        }


        public bool anyCollisions(isCollidable c)
        {
            for (int n = 0; n < _collidables.Count; n++)
            {
                if (c != _collidables[n])
                {
                    if (haveCollided(c, _collidables[n]))
                        return true;
                }
            }

            return false;
        }
    }
}
