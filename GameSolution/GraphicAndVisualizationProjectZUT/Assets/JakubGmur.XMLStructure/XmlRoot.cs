using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.JakubGmur.XMLStructure
{
    [Serializable]
    public class XmlRoot
    {
        [XmlElement("Players")]
        public List<SerializablePlayer> Players { get; set; }

        public XmlRoot()
        {
            Players = new List<SerializablePlayer>();
        }

        public XmlRoot(List<SerializablePlayer> players)
        {
            Players = players;
        }
    }
}
