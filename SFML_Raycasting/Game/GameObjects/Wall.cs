using System;

namespace Raycasting
{
    [Serializable]
    class Wall : GameObject
    {
        public Vector A { get; set; }
        public Vector B { get; set; }

        public Wall(float x1, float y1, float x2, float y2)
        {
            A = new Vector(x1, y1);
            B = new Vector(x2, y2);

            Texture = Textures.WallTexture;
        }

        public override (Vector A, Vector B) GetLine(Vector observedFrom) => (A, B);
    }
}
