using System;
using System.Collections.Generic;

namespace Raycasting
{
    [Serializable]
    class Map
    {
        public Map()
        {
            Objects = new List<GameObject>();
            StartPosition = new Vector(0, 0);
        }

        public List<GameObject> Objects { get; set; }

        public Vector StartPosition { get; private set; }

        public void InitializeTestMap()
        {
            float size = 10f;

            Objects.Add(new Wall(0,      0,      size,   0   ));
            Objects.Add(new Wall(size,   0,      size,   size));
            Objects.Add(new Wall(size,   size,   0,      size));
            Objects.Add(new Wall(0,      size,   0,      0   ));
            for (int i = 0; i < 0; i++)
            {
                Objects.Add(new Wall(0, size, 0, 0));
            }

            StartPosition = new Vector(0.5f, 0.5f);
        }
    }
}