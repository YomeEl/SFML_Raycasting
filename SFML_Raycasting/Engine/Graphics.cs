using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;

namespace Raycasting
{
    class Graphics
    {
        RenderWindow win;
        private Ray[] rays;

        public Graphics(RenderWindow win)
        {
            UpdateWindow(win);
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
                var r = new Ray(player.Position, new Vector(dir));
                rays[i] = r;
                dir.Rotate(Settings.Player.FOV / width);
                if (i % 10 == 0)
                {
                    dir.MakeUnit();
                }
            }
        }

        public void UpdateWindow(RenderWindow win)
        {
            this.win = win;
        }

        public void Draw(Player player, GameObject[] objects)
        {
            float width = win.Size.X;
            float height = win.Size.Y;

            var sky = new RectangleShape(new Vector2f(width, height / 2))
            {
                FillColor = new Color(135, 206, 235),
                Position = new Vector2f(0, 0)
            };
            var floor = new RectangleShape(new Vector2f(width, height / 2))
            {
                FillColor = new Color(100, 100, 100),
                Position = new Vector2f(0f, height / 2)
            };

            win.Draw(floor);
            win.Draw(sky);

            CreateRays((int)width, player);

            RectangleShape rect = new RectangleShape(new Vector2f((int)Settings.Drawing.Quality, 0));
            rect.Texture = Textures.WallTexture;

            for (int i = 0; i < rays.Length; i++)
            {
                (Vector intersection, float u) closestIntersectionInfo = (null, 0);
                Wall closestWall = null;
                float dist_to_closest = float.MaxValue;
                foreach (Wall w in objects)
                {
                    var intersection_info = rays[i].CastToWall(w);
                    if (intersection_info.intersection != null && 
                        (closestIntersectionInfo.intersection == null || 
                        Vector.Distance(player.Position, intersection_info.intersection) < dist_to_closest))
                    {
                        closestIntersectionInfo = intersection_info;
                        closestWall = w;
                        dist_to_closest = Vector.Distance(player.Position, closestIntersectionInfo.intersection);
                    }
                }
                if (closestIntersectionInfo.intersection != null)
                {
                    float dist = Vector.Distance(player.Position, closestIntersectionInfo.intersection);
                    dist *= Vector.Cos(rays[i].Direction, player.Rotation);

                    int left = (int)(Textures.WallTexture.Size.X * closestIntersectionInfo.u);
                    int w = (int)rect.Size.X;
                    int h = (int)Textures.WallTexture.Size.Y;
                    rect.TextureRect = new IntRect(left, 0, w, h);

                    float wallHeight = Settings.Drawing.WallHeight / dist;
                    float center = win.Size.Y / 2;
                    rect.Position = new Vector2f(i * (int)Settings.Drawing.Quality, center - wallHeight / 2);
                    rect.Size = new Vector2f(rect.Size.X, wallHeight);

                    win.Draw(rect);
                }
            }
        }
    }
}