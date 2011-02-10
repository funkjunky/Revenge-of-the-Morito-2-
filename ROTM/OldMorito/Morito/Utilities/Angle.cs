using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Morito.Utilities
{
    public struct Angle
    {
        private float _angle;


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
