using SFML.Graphics;
using System;

namespace Raycasting
{
    [Serializable]
    abstract class GameObject
    {
        [NonSerialized]
        Texture texture;

        public Texture Texture { get => texture; set => texture = value; }

        public abstract (Vector A, Vector B) GetLine(Vector observedFrom);

        public abstract Drawable EditorDraw(Vector cameraPos, float scale);
    }
}
