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
        private Graphics graphics;

        public Game(SFML.Graphics.RenderWindow app)
        {
            Serializer.Deserialize(out map, "map1.dat");
            map.RestoreColors();

            player = new Player(map.StartPosition);

            MovePlayer = player.Move;
            RotatePlayer = player.Rotation.Rotate;

            graphics = new Graphics(app);
        }

        public void toggleColors()
        {
            foreach (Wall w in map.Objects)
            {
                w.toggleColors = !w.toggleColors;
            }
        }

        public void Draw()
        {
            graphics.Draw(player, map.Objects.ToArray());
        }
    }
}