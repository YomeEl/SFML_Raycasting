using System;

namespace Raycasting
{
    [Serializable]
    class Wall : GameObject
    {
        private Vector a, b;

        public Wall(float x1, float y1, float x2, float y2)
        {
            a = new Vector(x1, y1);
            b = new Vector(x2, y2);
        }

        public Vector A { get => a; set => a = value; }

        public Vector B { get => b; set => b = value; }
    }
}
