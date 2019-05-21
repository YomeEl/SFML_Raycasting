using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace Raycasting
{
    [Serializable]
    class MyColor
    {
        [NonSerialized]
        public Color Color;

        public byte R, G, B, A;
        public MyColor(Color c)
        {
            Color = c;
           
            R = c.R;
            G = c.G;
            B = c.B;
            A = c.A;
        }

        public void Restore()
        {
            Color = new Color(R, G, B, A);
        }
    }

    [Serializable]
    class Wall : GameObject
    {
        private Vector a, b;

        private MyColor color;
        private List<MyColor> colors = new List<MyColor>();
        
        [NonSerialized]
        public bool toggleColors = true;

        public Wall(Color color, float x1, float y1, float x2, float y2)
        {
            A = new Vector(x1, y1);
            B = new Vector(x2, y2);

            this.color = new MyColor(color);

            colors.Add(new MyColor(SFML.Graphics.Color.Magenta));
            colors.Add(new MyColor(SFML.Graphics.Color.Blue));
            colors.Add(new MyColor(SFML.Graphics.Color.Black));
            colors.Add(new MyColor(SFML.Graphics.Color.Yellow));
            colors.Add(new MyColor(SFML.Graphics.Color.Red));
            colors.Add(new MyColor(SFML.Graphics.Color.Green));
        }

        public Vector A
        {
            get
            {
                return a;
            }

            set
            {
                a = value;
            }
        }

        public Vector B
        {
            get
            {
                return b;
            }

            set
            {
                b = value;
            }
        }

        public MyColor Color
        {
            get
            {
                return color;
            }
        }

        public List<MyColor> Colors
        {
            get
            {
                return colors;
            }
        }

        [NonSerialized]
        float u;
        public void Draw(RenderWindow app, int horPos, float height, float u)
        {
            this.u = u;
            Draw(app, horPos, height);
        }

        public void Draw(RenderWindow app, int horPos, float height)
        {
            float center = app.GetView().Center.Y;

            if (toggleColors)
            {
                Vertex[] line = new Vertex[2 * (int)Settings.Drawing.Quality];
                for (int i = 0; i < (int)Settings.Drawing.Quality; i++)
                {
                    Color col = colors[(int)((colors.Count - 1) * u)].Color;
                    line[2 * i] = new Vertex(new Vector2f(horPos + i, center + height / 2), col);
                    line[2 * i + 1] = new Vertex(new Vector2f(horPos + i, center - height / 2), col);
                }
                app.Draw(line, 0, (uint)line.Length, PrimitiveType.Lines);
            }
            else
            {
                Vertex[] line = new Vertex[2 * (int)Settings.Drawing.Quality];
                for (int i = 0; i < (int)Settings.Drawing.Quality; i++)
                {
                    line[0] = new Vertex(new Vector2f(horPos, center + height / 2), color.Color);
                    line[1] = new Vertex(new Vector2f(horPos, center - height / 2), color.Color);
                }
                app.Draw(line, 0, (uint)line.Length, PrimitiveType.Lines);
            }
        }
    }
}
