using SFML.Graphics;

namespace Raycasting
{
    class Textures
    {
        public static Texture WallTexture;

        public static void Load()
        {
            WallTexture = new Texture("Textures/Wall.jpg");
        }
    }
}