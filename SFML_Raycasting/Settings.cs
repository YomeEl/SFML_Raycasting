using System;

namespace Raycasting
{
    static class Settings
    {
        public static class Drawing
        {
            public static QualitySettings Quality = QualitySettings.High;
            public static int WallHeight;
        }

        public static class Player
        {
            public const float FOV = (float)(Math.PI / 2.5);
            public const float InitialSpeed = 0.01f;
        }
    }
}
