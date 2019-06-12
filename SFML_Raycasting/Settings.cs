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
        }

        public static class Menu
        {
            public static Font MenuFont = new Font(Properties.Resources.ALGER);
            public static Color SelectedColor = Color.Green;
            public static Color NotSelectedColor = Color.Red;
        }

        public static class Player
        {
            public const float FOV = (float)(Math.PI / 2.5);
            public const float InitialSpeed = 0.01f;
        }
    }
}
