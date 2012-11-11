using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Immersion
{
    [Serializable]
    public class MapData
    {
        public String name = "New Map";
        public List<PlatformData> Platforms = new List<PlatformData>();

        public static MapData ReadFromFile(String path)
        {
            BinaryFormatter bf = new BinaryFormatter();
            Stream s = File.Open(path, FileMode.Open);
            MapData map = (MapData)bf.Deserialize(s);
            s.Close();
            return map;
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
