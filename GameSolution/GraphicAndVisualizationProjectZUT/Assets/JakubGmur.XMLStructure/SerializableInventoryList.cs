using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.JakubGmur.XMLStructure
{
    [Serializable]
    public class SerializableInventoryList
    {
        public SerializableInventoryList()
        {
            List = new List<SerializablePickable>();
        }

        public SerializableInventoryList(List<SerializablePickable> pickableList)
        {
            List = pickableList;
        }

        [XmlElement(typeof(SerializablePickable), ElementName = "Item")]
        [XmlElement(typeof(SerializablePickableKey), ElementName = "Key")]
        public List<SerializablePickable> List { get; set; }
    }
}
