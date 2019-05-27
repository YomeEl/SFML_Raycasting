﻿namespace Raycasting
{
    class Game
    {
        public Map map;

        public delegate void DMovePlayer(Direction dir, int dist);
        public delegate void DRotatePlayer(float rad);
        public delegate void DUpdateApp(SFML.Graphics.RenderWindow app);

        public DMovePlayer MovePlayer;
        public DRotatePlayer RotatePlayer;
        public DUpdateApp UpdateApp;

        private Player player;
        private Graphics graphics;

        public Game(SFML.Graphics.RenderWindow app)
        {
            Serializer.Deserialize(out map, "map1.dat");
            map.RestoreColors();

            player = new Player(map.StartPosition);
            graphics = new Graphics(app);

            MovePlayer = player.Move;
            RotatePlayer = player.Rotation.Rotate;
            UpdateApp = graphics.UpdateApp;
        }

        public void toggleTextures()
        {
            foreach (Wall w in map.Objects)
            {
                w.toggleTextures = !w.toggleTextures;
            }
        }

        public void Draw()
        {
            graphics.Draw(player, map.Objects.ToArray());
        }
    }
}