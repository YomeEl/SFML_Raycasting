using System;

namespace Raycasting
{
    [Serializable]
    class Player
    {
        private float speed = Settings.Player.InitialSpeed;

        public Vector Position { get; set; }

        public Vector Rotation { get; private set; }

        public Player(Vector position)
        {
            this.Position = position;
            Rotation = new Vector(1, 0);
        }

        public Player(Vector position, Vector rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        public void LookAt(Vector coord)
        {
            Rotation = coord - Position;
        }

        public void LookAt(float x, float y)
        {
            LookAt(new Vector(x, y));
        }

        public void Move(Direction dir, int dist)
        {
            switch (dir)
            {
                case Direction.Forward:
                    Position += Rotation * speed * dist;
                    break;

                case Direction.Right:
                    Vector left_rot = new Vector(Rotation);
                    left_rot.Rotate((float)Math.PI / 2);
                    Position += left_rot * speed * dist;
                    break;

                case Direction.Backward:
                    Position -= Rotation * speed * dist;
                    break;

                case Direction.Left:
                    Vector right_rot = new Vector(Rotation);
                    right_rot.Rotate(-(float)Math.PI / 2);
                    Position += right_rot * speed * dist;
                    break;
            }
        }
    }
}