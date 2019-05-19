using System;
using System.Collections.Generic;
using SFML.Graphics;

namespace Raycasting
{
    [Serializable]
    class Map
    {
        private Vector startPosition;

        private List<GameObject> objects;

        public Map()
        {
            objects = new List<GameObject>();
            startPosition = new Vector(0, 0);
        }

        public List<GameObject> Objects
        {
            get
            {
                return objects;
            }

            set
            {
                objects = value;
            }
        }

        public Vector StartPosition
        {
            get
            {
                return startPosition;
            }
        }

        public void Test()
        {
            float size = 10f;

            objects.Add(new Wall(Color.Red,     0,      0,      size,   0   ));
            objects.Add(new Wall(Color.Green,   size,   0,      size,   size));
            objects.Add(new Wall(Color.Blue,    size,   size,   0,      size));
            objects.Add(new Wall(Color.White,   0,      size,   0,      0   ));
            for (int i = 0; i < 0; i++)
            {
                objects.Add(new Wall(Color.White, 0, size, 0, 0));
            }

            startPosition = new Vector(0.5f, 0.5f);
        }

        public void RestoreColors()
        {
            foreach (Wall w in objects)
            {
                w.Color.Restore();
                foreach (MyColor c in w.Colors)
                {
                    c.Restore();
                }
            }
        }
    }
}