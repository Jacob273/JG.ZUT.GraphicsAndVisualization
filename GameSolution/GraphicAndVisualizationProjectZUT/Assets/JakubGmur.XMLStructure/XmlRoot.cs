using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Assets.JakubGmur.XMLStructure
{
    [Serializable]
    public class XmlRoot
    {
        [XmlElement("Players")]
        public SerializablePlayerList Players { get; set; }

        public XmlRoot()
        {
            Players = new SerializablePlayerList();
        }

        public XmlRoot(List<SerializablePlayer> players)
        {
            Players = new SerializablePlayerList(players);
        }
    }
}
