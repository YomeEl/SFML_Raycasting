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
            if (rays == null || rays.Length != width + 1)
            {
                rays = new Ray[width + 1];
            }

            for (int i = 0; i <= width; i++)
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

            var sky = new RectangleShape(new Vector2f(width, height / 2));
            sky.FillColor = new Color(135, 206, 235);
            sky.Position = new Vector2f(0, 0);

            var floor = new RectangleShape(new Vector2f(width, height / 2));
            floor.FillColor = new Color(100, 100, 100);
            floor.Position = new Vector2f(0f, height / 2);

            app.Draw(floor);
            app.Draw(sky);

            CreateRays((int)width, player);

            RectangleShape rect = new RectangleShape(new Vector2f((int)Settings.Drawing.Quality, 0));
            rect.Texture = Textures.WallTexture;
            float u = 0;
            foreach (Ray r in rays)
            {
                Vector closestIntersection = null;
                Wall closestWall = null;
                foreach (Wall w in objects)
                {
                    float _u;
                    Vector intersection = r.CastToWall(w, out _u);
                    if (intersection != null && (closestIntersection == null || Vector.Distance(player.Position, intersection) < Vector.Distance(player.Position, closestIntersection)))
                    {
                        closestIntersection = intersection;
                        closestWall = w;
                        u = _u;
                    }
                }
                if (closestIntersection != null)
                {
                    float dist = Vector.Distance(player.Position, closestIntersection);
                    dist *= Vector.Cos(r.Direction, player.Rotation);

                    int left = (int)(Textures.WallTexture.Size.X * u);
                    int w = (int)rect.Size.X;
                    int h = (int)Textures.WallTexture.Size.Y;
                    rect.TextureRect = new IntRect(left, 0, w, h);

                    float wallHeight = Settings.Drawing.WallHeight / dist;
                    float center = app.Size.Y / 2;
                    rect.Position = new Vector2f(r.X, center - wallHeight / 2);
                    rect.Size = new Vector2f(rect.Size.X, wallHeight);

                    app.Draw(rect);
                }
            }
        }
    }
}