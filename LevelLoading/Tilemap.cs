using System;
using System.IO;
using Newtonsoft.Json;

namespace LevelTilemap
{
    public class Tilemap
    {
        public int tileWidth { get; set; }
        public int tileHeight { get; set; }
        public int tileScale { get; set; }
        public int[] checkpointXPositions { get; set; }

        public Entity[] entities { get; set; }
        public Tile[] tiles { get; set; }
        public Item[] items { get; set; }
        public Scenery[] scenery { get; set; }

        public class Entity
        {
            public string entityType { get; set; }
            public int[][] positions { get; set; }
            public string[] stateAtIndex { get; set; }
            public bool[] facingRightAtIndex { get; set; }
        }
        public class Tile
        {
            public string tileType { get; set; }
            public int[][] positions { get; set; }
            public string[] itemAtIndex { get; set; }
            public bool[] hiddenAtIndex { get; set; }
            public int[] coinsAtIndex { get; set; }
            public bool[] canTeleportAtIndex { get; set; }
        }
        public class Item
        {
            public string itemType { get; set; }
            public int[][] positions { get; set; }
        }
        public class Scenery
        {
            public string sceneryType { get; set; }
            public int[][] positions { get; set; }
        }


        public static Tilemap ReadJsonFile(string path)
        {
            Tilemap level;
            using (StreamReader sr = new StreamReader(path))
            {
                string readJson = sr.ReadToEnd();
                level = JsonConvert.DeserializeObject<Tilemap>(readJson);
            }
            return level;
        }
    }

    
}

