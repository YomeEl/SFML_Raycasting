using System;
using SFML.Graphics;

namespace Raycasting
{
    static class Settings
    {
        public static class Drawing
        {
            public static QualitySettings Quality = QualitySettings.High;
            public static uint WallHeight;
            public const float EnemyWidth = 0.5f;
        }

        public static class Menu
        {
            public static Font Font = new Font(Properties.Resources.Volkhov_Regular);
            public const uint FontSize = 64;
            public const uint Gap = 10;
            public static Color SelectedColor = Color.Yellow;
            public static Color NotSelectedColor = Color.Red;
            public static Color DisabledColor = new Color(150, 128, 128);
        }

        public static class Player
        {
            public const float FOV = (float)(Math.PI / 2.5);
            public const float InitialSpeed = 0.01f;
        }
    }
}
