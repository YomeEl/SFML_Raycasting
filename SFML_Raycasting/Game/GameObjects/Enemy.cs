using System;
using SFML.Graphics;

namespace Raycasting
{
    [Serializable]
    class Enemy : GameObject
    {
        public Vector Position { get; }

        public Enemy(Vector position)
        {
            Position = position;

            Texture = Textures.EnemyTexture;
        }

        public override (Vector A, Vector B) GetLine(Vector observedFrom)
        {
            Vector perp = Vector.GetPerpendicular(Position - observedFrom);
            perp.MakeUnit();
            perp = perp * (Settings.Drawing.EnemyWidth / 2);
            return (Position - perp, Position + perp);
        }

        public override Drawable EditorDraw(Vector cameraPos, float scale)
        {
            float d = scale;
            var cs = new CircleShape(d)
            {
                FillColor = Color.Red,
                Position = new SFML.System.Vector2f((Position - cameraPos).X, (Position - cameraPos).Y) * scale
            };
            cs.Position -= new SFML.System.Vector2f(1, 1) * d / 2;
            return cs;
        }
    }
}
