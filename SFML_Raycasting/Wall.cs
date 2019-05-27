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
        public bool toggleTextures = true;

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

        public Vertex[] GetVertices(int viewHeight, int horPos, int height, float u)
        {
            float center = viewHeight / 2;

            var vertices = new Stack<Vertex>();

            if (toggleTextures)
            {
                int h;
                int shift;
                if (height > viewHeight)
                {
                    h = viewHeight;
                    shift = (height - viewHeight) / 2;
                }
                else
                {
                    h = height;
                    shift = 0;
                }
                int x = (int)(Textures.WallTexture.GetLength(0) * u);
                if (u == 1)
                {
                    x--;
                }
                float pos = center - h / 2;
                float k = Textures.WallTexture.GetLength(1) / (float)height;
                Vector2f lastpos = new Vector2f();
                for (int i = 0; i < (int)Settings.Drawing.Quality; i++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        Color col = Textures.WallTexture[x, (int)((j + shift) * k)];
                        if (vertices.Count == 0 || vertices.Peek().Color != col)
                        {
                            if (vertices.Count != 0)
                            {
                                vertices.Push(new Vertex(new Vector2f(horPos + i, pos + j), vertices.Peek().Color));
                            }
                            vertices.Push(new Vertex(new Vector2f(horPos + i, pos + j), col));
                        }
                        lastpos.X = horPos + i;
                        lastpos.Y = pos + j;
                    }
                }
                if (vertices.Count % 2 != 0)
                {
                    vertices.Push(new Vertex(lastpos, vertices.Peek().Color));
                }
            }
            else
            {
                for (int i = 0; i < (int)Settings.Drawing.Quality; i++)
                {
                    vertices.Push(new Vertex(new Vector2f(horPos, center + height / 2), color.Color));
                    vertices.Push(new Vertex(new Vector2f(horPos, center - height / 2), color.Color));
                }
            }

            return vertices.ToArray();
        }
    }
}
