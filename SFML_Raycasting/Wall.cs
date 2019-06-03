using System;

namespace Raycasting
{
    [Serializable]
    class Wall : GameObject
    {
        private Vector a, b;
        private float length;

        public Wall(float x1, float y1, float x2, float y2)
        {
            a = new Vector(x1, y1);
            b = new Vector(x2, y2);

            length = Vector.Distance(a, b);
        }

        public Vector A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public Vector B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public float Length
        {
            get
            {
                return length;
            }
        }
    }
}
