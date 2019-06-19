using System;

namespace Raycasting
{
    /// <summary>
    /// This class represents mathematical vector and some methods to work with it
    /// </summary>
    [Serializable]
    class Vector
    {
        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector(Vector a)
        {
            X = a.X;
            Y = a.Y;
        }

        public Vector()
        {
            X = 0;
            Y = 0;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float GetLength()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public static Vector operator +(Vector left, Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y);
        }

        public static Vector operator -(Vector left, Vector right)
        {
            return new Vector(left.X - right.X, left.Y - right.Y);
        }

        public static Vector operator *(Vector left, float right)
        {
            return new Vector(left.X * right, left.Y * right);
        }

        /// <summary>
        /// Returns cosine of angle between two vectors
        /// </summary>
        /// <param name="a">First vector</param>
        /// <param name="b">Second vector</param>
        /// <returns>Cosine between a and b</returns>
        public static float Cos(Vector a, Vector b)
        {
            return (a.X * b.X + a.Y * b.Y) / (a.GetLength() * b.GetLength());
        }

        /// <summary>
        /// Makes vector unit
        /// </summary>
        public void MakeUnit()
        {
            float length = GetLength();
            X /= length;
            Y /= length;
        }

        /// <summary>
        /// Rotates vector by some angle
        /// </summary>
        /// <param name="rad">Angle of turn in radians</param>
        public void Rotate(float rad)
        {
            float sin = (float)Math.Sin(rad);
            float cos = (float)Math.Cos(rad);

            float new_x = cos * X - sin * Y;
            float new_y = sin * X + cos * Y;

            X = new_x;
            Y = new_y;
        }

        /// <summary>
        /// Returns distance between two points
        /// </summary>
        /// <param name="a">Point A</param>
        /// <param name="b">Point B</param>
        /// <returns>Distance between A and B</returns>
        public static float Distance(Vector a, Vector b)
        {
            return (b - a).GetLength();
        }
    }
}
