using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;

namespace Raycasting
{
    class Graphics
    {
        private RenderWindow win;
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
                rays[i] = new Ray(player.Position, new Vector(dir));
                dir.Rotate(Settings.Player.FOV / width);
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
            for (int i = 0; i < rays.Length; i++)
            {
                var entitiesIntersections = new List<(Vector intersection, float u, GameObject gameObject)>();
                (Vector intersection, float u) closestWallInfo = (null, 0);
                GameObject closestObject = null;
                float distToClosestWall = float.MaxValue;
                foreach (GameObject obj in objects)
                {
                    var intersectionInfo = rays[i].CastTo(obj);
                    if (intersectionInfo.intersection != null)
                    {
                        if (obj is Wall)
                        {
                            if (closestWallInfo.intersection == null ||
                                Vector.Distance(player.Position, intersectionInfo.intersection) < distToClosestWall)
                            {
                                closestWallInfo = intersectionInfo;
                                closestObject = obj;
                                distToClosestWall = Vector.Distance(player.Position, closestWallInfo.intersection);
                            }
                        }
                        else
                        {
                            entitiesIntersections.Add((intersectionInfo.intersection, intersectionInfo.u, obj));
                        }
                    }
                }

                var invisibleObjects = new List<(Vector intersection, float u, GameObject gameObject)>();
                if (closestWallInfo.intersection != null)
                {
                    foreach (var intersectionInfo in entitiesIntersections)
                    {
                        if (Vector.Distance(player.Position, intersectionInfo.intersection) > distToClosestWall)
                        {
                            invisibleObjects.Add(intersectionInfo);
                        }
                    }
                
                    rect.Texture = closestObject.Texture;
                    int left = (int)(rect.Texture.Size.X * closestWallInfo.u);
                    int w = (int)rect.Size.X;
                    int h = (int)rect.Texture.Size.Y;
                    rect.TextureRect = new IntRect(left, 0, w, h);

                    float wallHeight = Settings.Drawing.WallHeight / distToClosestWall / Vector.Cos(rays[i].Direction, player.Rotation);
                    float center = win.Size.Y / 2;
                    rect.Position = new Vector2f(i * (int)Settings.Drawing.Quality, center - wallHeight / 2);
                    rect.Size = new Vector2f(rect.Size.X, wallHeight);

                    win.Draw(rect);
                }

                foreach (var intersectionInfo in entitiesIntersections)
                {
                    if (intersectionInfo.intersection != null && !invisibleObjects.Contains(intersectionInfo))
                    {
                        float dist = Vector.Distance(player.Position, intersectionInfo.intersection);

                        rect.Texture = intersectionInfo.gameObject.Texture;
                        int left = (int)(rect.Texture.Size.X * intersectionInfo.u);
                        int w = (int)rect.Size.X;
                        int h = (int)rect.Texture.Size.Y;
                        rect.TextureRect = new IntRect(left, 0, w, h);

                        float wallHeight = Settings.Drawing.WallHeight / dist;
                        float center = win.Size.Y / 2;
                        rect.Position = new Vector2f(i * (int)Settings.Drawing.Quality, center - wallHeight / 2);
                        rect.Size = new Vector2f(rect.Size.X, wallHeight);

                        win.Draw(rect);
                    }
                }
            }

            var gun = new RectangleShape(new Vector2f(win.Size.X, win.Size.Y));
            gun.Texture = Textures.Gun;
            win.Draw(gun);

            var cross = new Vertex[4];
            var wincenter = win.GetView().Center;
            cross[0] = new Vertex(wincenter - new Vector2f(10, 0), Color.Red);
            cross[1] = new Vertex(wincenter - new Vector2f(-10, 0), Color.Red);
            cross[2] = new Vertex(wincenter - new Vector2f(0, 10), Color.Red);
            cross[3] = new Vertex(wincenter - new Vector2f(0, -10), Color.Red);
            win.Draw(cross, PrimitiveType.Lines);
        }
    }
}