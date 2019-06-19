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
            map = new Map();
            map.InitializeTestMap();
            Serializer.Serialize(map, "Maps/map1.dat");

            player = new Player(map.StartPosition);
            graphics = new Graphics(win);

            MovePlayer = player.Move;
            RotatePlayer = player.Rotation.Rotate;
            UpdateWindow = graphics.UpdateWindow;
        }

        public void Draw()
        {
            graphics.Draw(player, map.Objects.ToArray());
        }
    }
}