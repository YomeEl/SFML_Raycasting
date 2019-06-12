using System;

namespace Raycasting
{
    [Serializable]
    class Player
    {
        private Vector position;
        private Vector rotation;
        private float speed = Settings.Player.InitialSpeed;

        public Vector Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Vector Rotation
        {
            get
            {
                return rotation;
            }
        }

        public float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public Player(Vector position)
        {
            this.position = position;
            this.rotation = new Vector(1, 0);
        }

        public Player(Vector position, Vector rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }

        public void LookAt(Vector coord)
        {
            rotation = coord - position;
            rotation.MakeUnit();
        }

        public void LookAt(float x, float y)
        {
            LookAt(new Vector(x, y));
        }

        public void MoveTo(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        public void Move(Direction dir, int dist)
        {
            switch (dir)
            {
                case Direction.Forward:
                    position += rotation * speed * dist;
                    break;

                case Direction.Right:
                    Vector left_rot = new Vector(rotation);
                    left_rot.Rotate((float)Math.PI / 2);
                    position += left_rot * speed * dist;
                    break;

                case Direction.Backward:
                    position -= rotation * speed * dist;
                    break;

                case Direction.Left:
                    Vector right_rot = new Vector(rotation);
                    right_rot.Rotate(-(float)Math.PI / 2);
                    position += right_rot * speed * dist;
                    break;
            }
        }
    }
}
