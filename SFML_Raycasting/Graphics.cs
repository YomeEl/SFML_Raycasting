using SFML.Graphics;
using SFML.System;

namespace Raycasting
{
    class Graphics
    {
        RenderWindow app;
        private Ray[] rays;

        public Graphics(RenderWindow app)
        {
            UpdateApp(app);
        }

        private void CreateRays(int width, Player player)
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

        public void UpdateApp(RenderWindow app)
        {
            this.app = app;
        }

        public void Draw(Player player, GameObject[] objects)
        {
            float width = app.Size.X;
            float height = app.Size.Y;

            var rect = new RectangleShape(new Vector2f(width, height / 2));
            rect.FillColor = new Color(100, 100, 100);
            rect.Position = new Vector2f(0f, height / 2);

            app.Draw(rect);

            CreateRays((int)width, player);

            foreach (Ray r in rays)
            {
                Vector closestIntersection = null;
                Wall closestWall = null;
                foreach (Wall w in objects)
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