using System;

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
    }
}
