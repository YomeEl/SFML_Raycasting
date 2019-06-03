namespace Raycasting
{
    class Ray
    {
        Vector pos;
        Vector dir;

        private int x;

        public Vector Direction
        {
            get
            {
                return dir;
            }
        }

        public int X
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

        public Ray(Vector pos, Vector dir)
        {
            this.pos = pos;
            this.dir = dir;

            this.dir.MakeUnit();
        }

        public Ray(float x1, float y1, float x2, float y2)
        {
            pos = new Vector(x1, y1);
            dir = new Vector(x2, y2);
        }

        public Vector CastToWall(Wall wall, out float _u)
        {
            float x1 = pos.X;
            float y1 = pos.Y;
            float x2 = pos.X + dir.X * 1000;
            float y2 = pos.Y + dir.Y * 1000;

            float x3 = wall.A.X;
            float y3 = wall.A.Y;
            float x4 = wall.B.X;
            float y4 = wall.B.Y;

            float den = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

            if (den == 0)
            {
                _u = 0;
                return null;
            }

            float t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / den;
            float u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / den;

            if (u >= 0 && u <= 1 && t >= 0)
            {
                _u = u;
                return new Vector(x1 + t * (x2 - x1), y1 + t * (y2 - y1));
            }
            else
            {
                _u = 0;
                return null;
            }
        }
    }
}
