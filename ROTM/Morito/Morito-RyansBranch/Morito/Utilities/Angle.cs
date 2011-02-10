using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Morito
{
    public struct Angle
    {
        private float _angle;

        #region Constructors
        public Angle(float angle)
        {
            _angle = 0;
            Value = angle;
        }
        public Angle(double angle)
            : this((float)angle)
        {

        }
        #endregion

        public float Value
        {
            get { return _angle; }
            set 
            { 
                _angle = value;

                if (_angle < 0)
                    _angle = (float)(Math.PI * 2) + (_angle % (float)(Math.PI * 2));

                if (_angle > Math.PI * 2)
                    _angle %= (float)(Math.PI * 2);
            }
        }

        // Returns true if angle is found between start (going counter clockwise) before end
        // So (3.14, 0, 6) = true 
        // And (0, 3, 6) = false
        public bool isAngleBetweenAngles(Angle start, Angle end)
        {
            float offset = start.Value;

            // For correcting the angles to 
            Angle tempAngle = new Angle(this.Value - offset);
            Angle tempStart = new Angle(start.Value - offset);
            Angle tempEnd   = new Angle(end.Value - offset);

            return (tempAngle.Value > tempStart.Value && tempAngle.Value < tempEnd.Value);
        }
    }

    public struct Angle3
    {
        private Angle _x;
        private Angle _y;
        private Angle _z;

        public float X
        {
            get { return _x.Value; }
            set { _x.Value = value; }
        }

        public float Y
        {
            get { return _y.Value; }
            set { _y.Value = value; }
        }

        public float Z
        {
            get { return _z.Value; }
            set { _z.Value = value; }
        }

        public Vector3 VectorAngle
        {
            get { return new Vector3(X, Y, Z); }
            set { X = value.X; Y = value.Y; Z = value.Z; }
        }
    }
}
