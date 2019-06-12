using System;

namespace Raycasting
{
    [Serializable]
    class Vector
    {
        private float x, y;

        public Vector(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector(Vector a)
        {
            this.x = a.x;
            this.y = a.y;
        }

        public Vector()
        {
            x = 0;
            y = 0;
        }

        public float X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public float Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public float GetLength()
        {
            return (float)Math.Sqrt(x * x + y * y);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.x + right.x, left.y + right.y);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.x - right.x, left.y - right.y);
        }

        public static Vector operator *(Vector left, float right)
        {
            return new Vector(left.x * right, left.y * right);
        }

        public static float Cos(Vector a, Vector b)
        {
            return (a.x * b.x + a.y * b.y) / (a.GetLength() * b.GetLength());
        }

        public void MakeUnit()
        {
            float length = GetLength();
            x /= length;
            y /= length;
        }

        public void Rotate(float rad)
        {
            float sin = (float)Math.Sin(rad);
            float cos = (float)Math.Cos(rad);

            float new_x = cos * x - sin * y;
            float new_y = sin * x + cos * y;

            x = new_x;
            y = new_y;
        }

        public static float Distance(Vector a, Vector b)
        {
            return (b - a).GetLength();
        }

        public override string ToString()
        {
            return $"x = {x}, y = {y}";
        }
    }
}
