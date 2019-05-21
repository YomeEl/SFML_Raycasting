using SFML.Graphics;

namespace Raycasting
{
    interface GameObject
    {
        void Draw(RenderWindow app, int horPos, float height);
    }
}
