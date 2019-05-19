using SFML.Graphics;
using SFML.System;

namespace Raycasting
{
    class Game
    {
        public Map map;

        public delegate void DMovePlayer(Direction dir, int dist);
        public delegate void DRotatePlayer(float rad);

        public DMovePlayer MovePlayer;
        public DRotatePlayer RotatePlayer;

        private Player player;
        private Ray[] rays;

        public Game()
        {
            Serializer.Deserialize(out map, "map1.dat");
            map.RestoreColors();

            player = new Player(map.StartPosition);

            MovePlayer = player.Move;
            RotatePlayer = player.Rotation.Rotate;
        }

        public void toggleColors()
        {
            foreach (Wall w in map.Objects)
            {
                w.toggleColors = !w.toggleColors;
            }
        }

        private void CreateRays(int width)
        {
            Vector dir = new Vector(player.Rotation);
            dir.Rotate(-Settings.Player.FOV / 2);

            width /= (int)Settings.Drawing.Quality;
            if (rays == null || rays.Length != width)
            {
                rays = new Ray[width];
            }

            for (int i = 0; i < width; i++)
            {
                Ray r = new Ray(player.Position, new Vector(dir));
                r.X = i * (int)Settings.Drawing.Quality;
                rays[i] = r;
                dir.Rotate(Settings.Player.FOV / width);
                if (i % 10 == 0)
                {
                    dir.MakeUnit();
                }
            }
        }

        public void Draw(RenderWindow app)
        {
            float width = app.DefaultView.Size.X;
            float height = app.DefaultView.Size.Y;

            RectangleShape rect = new RectangleShape(new Vector2f(width, height / 2));
            rect.FillColor = new Color(100, 100, 100);
            rect.Position = new Vector2f(0f, height / 2);

            app.Draw(rect);

            CreateRays((int)width);

            foreach (Ray r in rays)
            {
                Vector closestIntersection = null;
                Wall closestWall = null;
                foreach (Wall w in map.Objects)
                {
                    Vector intersection = r.CastToWall(w);
                    if (intersection != null && (closestIntersection == null || Vector.Distance(player.Position, intersection) < Vector.Distance(player.Position, closestIntersection)))
                    {
                        closestIntersection = intersection;
                        closestWall = w;
                    }
                }
                if (closestIntersection != null)
                { 
                    float u = Vector.Distance(closestWall.A, closestIntersection); 
                    u /= Vector.Distance(closestWall.A, closestWall.B);
                    float dist = Vector.Distance(player.Position, closestIntersection);
                    dist *= Vector.Cos(r.Direction, player.Rotation);
                    closestWall.Draw(app, r.X, Settings.Drawing.WallHeight / dist, u);
                }
            }
        }
    }
}