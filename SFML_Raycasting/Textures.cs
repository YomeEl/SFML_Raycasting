using SFML.Graphics;
using System.IO;

namespace Raycasting
{
    class Textures
    {
        public static Color[,] WallTexture;

        public static void Load()
        {
            var f = new FileStream("Textures/Wall.jpg", FileMode.Open);
            var im = new Image(f);
            WallTexture = new Color[im.Size.X, im.Size.Y];
            for (uint i = 0; i < im.Size.X; i++)
            {
                for (uint j = 0; j < im.Size.Y; j++)
                {
                    WallTexture[i, j] = im.GetPixel(i, j);
                }
            }
        }
    }
}
