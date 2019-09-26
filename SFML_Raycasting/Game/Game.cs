namespace Raycasting
{
    class Game
    {
        public Map map;

        public delegate void DMovePlayer(Direction dir, int dist);
        public delegate void DRotatePlayer(float rad);
        public delegate void DUpdateApp(SFML.Graphics.RenderWindow win);

        public DMovePlayer MovePlayer;
        public DRotatePlayer RotatePlayer;
        public DUpdateApp UpdateWindow;

        private Player player;
        private Graphics graphics;

        public Game(SFML.Graphics.RenderWindow win)
        {
            Serializer.Deserialize(out map, "Maps/map1.dat");

            foreach (GameObject obj in map.Objects)
            {
                switch (obj)
                {
                    case Wall w:
                        w.Texture = Textures.WallTexture;
                        break;

                    case Enemy e:
                        e.Texture = Textures.EnemyTexture;
                        break;
                }
            }

            player = new Player(map.StartPosition);
            graphics = new Graphics(win);

            MovePlayer = player.Move;
            RotatePlayer = player.Rotation.Rotate;
            UpdateWindow = graphics.UpdateWindow;
        }

        public void Shoot()
        {
            var bullet = new Ray(player.Position, player.Rotation);
            GameObject hit = null;
            foreach (GameObject obj in map.Objects)
            {
                if (!(obj is Wall))
                {
                    var intersectionInfo = bullet.CastTo(obj);
                    if (intersectionInfo.intersection != null)
                    {
                        hit = obj;
                    }
                }
            }
            if (hit != null)
            {
                //map.Objects.Remove(hit);
            }
        }

        public void Draw()
        {
            graphics.Draw(player, map.Objects);
        }
    }
}