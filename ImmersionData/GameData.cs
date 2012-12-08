using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Immersion
{
    [Serializable]
    public class GameData
    {
        public List<MapData> Maps = new List<MapData>();
        public MapData StartMap;
        public MapData LastEditedMap;

        public GameData() : this(new MapData()) { }

        public GameData(MapData map)
        {
            Maps.Add(map);
            StartMap = map;
        }


        public static GameData ReadFromFile(String path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(path, FileMode.Open);
            GameData game = (GameData)bf.Deserialize(s);
            s.Close();
            return game;
        }

        public void WriteToFile(String path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(path, FileMode.Create);
            bf.Serialize(s, this);
            s.Close();
        }
    }

}
