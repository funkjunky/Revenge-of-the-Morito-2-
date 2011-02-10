using System;
using System.Xml.Linq;
using Microsoft.Xna.Framework;

namespace Morito
{
    public static class Vector2Extender
    {
        /// <summary>
        /// This function returns true if the sign has changed since reduction.
        /// </summary>
        /// <param name="thisVector"></param>
        /// <param name="otherVector"></param>
        /// <returns></returns>
        public static Vector2 Reduce(this Vector2 thisVector, Vector2 otherVector)
        {
            //store the sign of each coordinate
            bool isPositiveX = thisVector.X > 0;
            bool isPositiveY = thisVector.Y > 0;

            //reduce X and Y
            thisVector.X = Math.Abs(thisVector.X) - otherVector.X;
            thisVector.Y = Math.Abs(thisVector.Y) - otherVector.Y;
            
            if (!isPositiveX) thisVector.X *= -1;
            if (!isPositiveY) thisVector.Y *= -1;

            //return true if the sign has changed.
            //return ((isPositiveX && thisVector.X < 0) 
            //        || (isPositiveY && thisVector.Y < 0) 
            //        || (!isPositiveX && thisVector.X > 0) 
            //        || (!isPositiveY && thisVector.Y > 0));
            return thisVector;
        }

        public static void Clamp(this Vector2 thisVector, float floor)
        {
            thisVector.Clamp(floor, 0f);
        }

        public static void Clamp(this Vector2 thisVector, float floor, float ceiling)
        {
            thisVector.X = MathHelper.Clamp(thisVector.X, (float)Math.Asin(floor), (float)Math.Asin(ceiling));
            thisVector.Y = MathHelper.Clamp(thisVector.Y, (float)Math.Acos(floor), (float)Math.Acos(ceiling));
        }

        //it's not length... arg... what the fck is it called... ugh my head...
        //this name isn't good either... bah... naming is tricky.
        public static Vector2 getDirectedVector(this Vector2 thisVector, float length)
        { 
            Vector2 tempVector = thisVector;
            tempVector.Normalize();
            return new Vector2(tempVector.X * length, tempVector.Y * length);
        }

        //This is how the fuck it should have bee done in the fucking first place... yeish...
        public static Vector2 goodNormalize(this Vector2 thisVector)
        {
            thisVector.Normalize();
            return thisVector;
        }

        // Simple function to reverse a vector's direction
        public static Vector2 reverseDirection(this Vector2 thisVector)
        {
            thisVector.X *= -1;
            thisVector.Y *= -1;

            return thisVector;
        }

        // Rotates the given vector by the given angle
        // angle is in radians
        public static Vector2 rotateByAngle(this Vector2 thisVector, Angle angle)
        {
            if ( angle.Value == 0 || (thisVector.X == 0 && thisVector.Y == 0) )
                return thisVector;

            float thisAngle = thisVector.getAngle().Value + angle.Value;
            float length = thisVector.Length();
            // TODO: Logic is messed up, make sure sin and cos can handle negative numbers
            return new Vector2( (float)(Math.Cos(thisAngle) * length), (float)(Math.Sin(thisAngle) * length) );
        }

        // Returns the angle in radians of the current vector
        // 0 radians is vector(1, 0)
        public static Angle getAngle(this Vector2 thisVector)
        {
            if (thisVector.Y == 0)
                return new Angle(thisVector.X > 0 ? 0 : (float)Math.PI);
            else if (thisVector.X == 0)
            {
                return new Angle(Math.Sign(thisVector.Y) * (Math.PI / 2));
            }
            else
            {
                Angle angle = new Angle(Math.Atan(thisVector.Y / thisVector.X));

                if (thisVector.X < 0)
                    angle.Value += (float)Math.PI;
                else
                    angle.Value += (float)Math.PI * 2;

                return angle;    
            }
        }

        public static Vector2 loadFromXElement(this Vector2 vector2, XElement objectXML)
        {
            vector2.X = Convert.ToSingle(objectXML.Attribute("X").Value);
            vector2.Y = Convert.ToSingle(objectXML.Attribute("Y").Value);

            return vector2;
        }

        public static XElement serializeToXElement(this Vector2 vector2)
        {
            return
                new XElement("Vector2",
                    new XAttribute("X", vector2.X),
                    new XAttribute("Y", vector2.Y)
                );
        }
    }
}
