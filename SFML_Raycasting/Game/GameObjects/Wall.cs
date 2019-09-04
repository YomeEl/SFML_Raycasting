using System;
using SFML.Graphics;
using SFML.System;

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

        public override Drawable EditorDraw(Vector cameraPos, float scale)
        {
            float length = Vector.Distance(A, B) * scale;
            float width = Math.Max(0.1f * scale, 1f);
            var rs = new RectangleShape(new Vector2f(length, width))
            {
                FillColor = Color.Black,
                Position = new Vector2f((A - cameraPos).X, (A - cameraPos).Y) * scale,
                Rotation = (float)(Math.Acos(Vector.Cos(B - A, new Vector(1, 0))) / Math.PI * 180)
            };

            var wall = new VertexArray(PrimitiveType.Lines);
            var vertexA = new Vertex(new Vector2f((A - cameraPos).X, (A - cameraPos).Y) * scale);
            var vertexB = new Vertex(new Vector2f((B - cameraPos).X, (B - cameraPos).Y) * scale);
            vertexA.Color = vertexB.Color = Color.Black;
            wall.Append(vertexA);
            wall.Append(vertexB);

            return wall;
        }
    }
}
