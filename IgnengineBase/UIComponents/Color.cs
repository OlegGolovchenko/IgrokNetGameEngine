using System;

namespace IgnengineBase.UIComponents
{

    public class Color
    {
        private float _red;
        public float R
        {
            get { return _red; }
            set { _red = value; }
        }
        
        private float _green;
        public float G
        {
            get { return _green; }
            set { _green = value; }
        }
        
        private float _blue;
        public float B
        {
            get { return _blue; }
            set { _blue = value; }
        }
        
        private float _alpha;
        public float A
        {
            get { return _alpha; }
            set { _alpha = value; }
        }
        

        public Color(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public void ApplyTransperency(float alpha){
            A = alpha;
        }

        public void Apply()
        {
            Natives.glColor4f(R, G, B, A);
        }

        public static Color White { get => new Color(1, 1, 1, 1); }
        public static Color Black { get => new Color(0, 0, 0, 1); }
        public static Color Red { get => new Color(1, 0, 0, 1); }
        public static Color Green { get => new Color(0, 1, 0, 1); }
        public static Color Blue { get => new Color(0, 0, 1, 1); }
    }
}