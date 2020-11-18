using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.JakubGmur.XMLStructure
{
    public class SerializablePlayerList
    {
        [XmlElement("Player")]
        public List<SerializablePlayer> List { get; set; }

        public SerializablePlayerList(List<SerializablePlayer> players)
        {
            List = players;
        }

        public SerializablePlayerList()
        {
            List = new List<SerializablePlayer>();
        }
    }
}
