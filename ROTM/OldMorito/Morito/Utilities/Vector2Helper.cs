using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Morito.Utilities
{
    static class Vector2Helper
    {
        public static Vector2 FindVectorToAngle(float angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public static float FindAngleToVector(Vector2 vector)
        {
            float a = (float)Math.Atan(vector.Y / vector.X);
            float result;
            if ( vector.X < 0 )
                result = a + (float)Math.PI;
            else
                result = a;

            Angle ang = new Angle();
            ang.Value = result;
            return ang.Value;
        }

        public static float FindDistanceOfVector(Vector2 vector)
        {
            return (float)Math.Sqrt((double)(vector.X * vector.X + vector.Y * vector.Y));
        }
    }
}
