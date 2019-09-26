using System;
using System.Collections.Generic;

namespace Raycasting
{
    [Serializable]
    class Map
    {
        public Map()
        {
            StartPosition = new Vector(0, 0);
        }

        public GameObject[] Objects { get; set; }

        public Vector StartPosition { get; private set; }

        public void InitializeTestMap()
        {
            float size = 10f;

            var mapObjects = new List<GameObject>();
            mapObjects.Add(new Wall(0,      0,      size,   0   ));
            mapObjects.Add(new Wall(size,   0,      size,   size));
            mapObjects.Add(new Wall(size,   size,   0,      size));
            mapObjects.Add(new Wall(0,      size,   0,      0   ));
            for (int i = 0; i < 100; i++)
            {
                mapObjects.Add(new Wall(0, size, 0, 0));
            }

            mapObjects.Add(new Enemy(new Vector(5, 5)));
            mapObjects.Add(new Enemy(new Vector(6, 6)));
            mapObjects.Add(new Enemy(new Vector(-5, -5)));

            Objects = mapObjects.ToArray();

            StartPosition = new Vector(0.5f, 0.5f);
        }
    }
}