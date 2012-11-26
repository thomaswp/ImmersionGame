using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Immersion
{
    [Serializable]
    public class ItemData
    {
        public string Name;
        public string ImageName;
        public bool EnablesSlide;

        public ItemData(string name, string imageName) 
        {
            this.Name = name;
            this.ImageName = imageName;
        }
    }

}
