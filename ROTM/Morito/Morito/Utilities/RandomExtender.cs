using System;
using Microsoft.Xna.Framework;

namespace Morito
{
    public static class RandomExtender
    {
        public static double nextSignedDouble(this Random random)
        {
            return (random.NextDouble() - 0.5f) * 2f;
        }

        public static float nextFloat(this Random random)
        {
            return (float)random.NextDouble();
        }

        public static float nextSignedFloat(this Random random)
        {
            return (float)random.nextSignedDouble();
        }

        // This function takes two numbers a and b
        // and returns a random number between them.
        public static float nextSignedFloatInterval(this Random random, float a, float b)
        {
            float n = random.nextFloat();

            float np = (n + 1) / 2;

            n = (b - a) * np;

            return (n + a);
        }

        public static Vector2 nextVector2Interval(this Random random, Vector2 v1, Vector2 v2)
        {
            return new Vector2(random.nextSignedFloatInterval(v1.X, v2.X),
                               random.nextSignedFloatInterval(v1.Y, v2.Y));
        }

        public static Vector3 nextVector3Interval(this Random random, Vector3 v1, Vector3 v2)
        {
            return new Vector3(random.nextSignedFloatInterval(v1.X, v2.X),
                               random.nextSignedFloatInterval(v1.Y, v2.Y),
                               random.nextSignedFloatInterval(v1.Z, v2.Z));
        }
    }
}
