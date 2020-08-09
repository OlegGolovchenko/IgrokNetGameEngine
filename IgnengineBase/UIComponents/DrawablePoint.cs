using System;

namespace IgnengineBase.UIComponents
{

    public class DrawablePoint
    {
        private float _x, _y, _z;
        public float X { get => _x; }
        public float Y { get => _y; }
        public float Z { get => _z; }
        internal DrawablePoint(float x, float y, float z)
        {
            _x = x;
            _y = y;
            _z = z;
        }

        public static DrawablePoint GetInstance(float x, float y, float z)
        {
            return new DrawablePoint(x, y, z);
        }
    }

}