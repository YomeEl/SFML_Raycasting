using SFML.Graphics;

namespace Raycasting
{
    class Textures
    {
        public static Texture WallTexture;
        public static Texture EnemyTexture;
        public static Texture Gun;
        public static void Load()
        {
            WallTexture = new Texture("Textures/Wall.jpg");

            Image enemyImage = new Image("Textures/Enemy.png");
            enemyImage.CreateMaskFromColor(enemyImage.GetPixel(0, 0), 0);
            EnemyTexture = new Texture(enemyImage);

            Image gunImage = new Image("Textures/Gun.png");
            gunImage.CreateMaskFromColor(gunImage.GetPixel(0, 0), 0);
            Gun = new Texture(gunImage);
        }
    }
}