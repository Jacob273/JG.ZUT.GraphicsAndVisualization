using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.JakubGmur.XMLStructure
{
    [Serializable]
    public class SerializableInventoryList
    {
        [XmlElement("InventoryList")]
        public List<SerializableInventoryItem> InventoryList { get; set; }
    }
}
