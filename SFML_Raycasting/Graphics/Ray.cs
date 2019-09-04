namespace Raycasting
{
    class Ray
    {
        private readonly Vector Position;

        public Vector Direction { get; }

        public Ray(Vector pos, Vector dir)
        {
            Position = pos;
            Direction = dir;
        }

        public (Vector intersection, float u) CastTo(GameObject target)
        {
            float x1 = Position.X;
            float y1 = Position.Y;
            float x2 = Position.X + Direction.X * 1000;
            float y2 = Position.Y + Direction.Y * 1000;

            var (A, B) = target.GetLine(Position);
            float x3 = A.X;
            float y3 = A.Y;
            float x4 = B.X;
            float y4 = B.Y;

            float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (den == 0)
            {
                return (null, 0);
            }

            float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;

            if (u >= 0 && u <= 1 && t >= 0)
            {
                return (new Vector(x1 + t * (x2 - x1), y1 + t * (y2 - y1)), u);
            }
            else
            {
                return (null, 0);
            }
        }
    }
}
