using SFML.Graphics;
using SFML.System;

namespace Raycasting
{
    class Editor
    {
        RenderWindow win;
        Map map;

        public Vector CameraPos { get; set; } = new Vector();

        public float Scale { get; set; } = 19.0f;

        public Editor(RenderWindow win)
        {
            UpdateWindow(win);
        }

        public void UpdateWindow(RenderWindow win)
        {
            this.win = win;
        }

        public void Open(Map map)
        {
            this.map = map;
            CameraPos = map.StartPosition;
        }

        public void Draw()
        {
            win.Clear(Color.White);
            var center = new Vector(win.GetView().Center.X, win.GetView().Center.Y) * (1 / Scale);

            foreach (GameObject obj in map.Objects)
            {
                win.Draw(obj.EditorDraw(CameraPos - center, Scale));
            }

            float r = 0.3f * Scale;
            var playerMarker = new CircleShape(r)
            {
                FillColor = Color.Green,
                Position = new Vector2f((map.StartPosition - CameraPos + center).X, (map.StartPosition - CameraPos + center).Y) * Scale
            };
            
            var playerVertex = new Vertex(playerMarker.Position, Color.Green);
            var playerDirectionVertex = new Vertex(playerMarker.Position + new Vector2f(1, 0) * 2 * r, Color.Green);
            var playerDirectionMarker = new VertexArray(PrimitiveType.Lines);
            playerDirectionMarker.Append(playerVertex);
            playerDirectionMarker.Append(playerDirectionVertex);

            playerMarker.Position -= new Vector2f(1, 1) * r;

            win.Draw(playerMarker);
            win.Draw(playerDirectionMarker);
        }
    }
}
