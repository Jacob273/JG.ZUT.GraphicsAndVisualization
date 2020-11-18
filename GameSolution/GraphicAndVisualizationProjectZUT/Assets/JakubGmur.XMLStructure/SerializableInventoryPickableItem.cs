using System;
using System.Xml.Serialization;

namespace Assets.JakubGmur.XMLStructure
{
    [Serializable]
    public class SerializablePickable
    {
        public SerializablePickable()
        {

        }

        public SerializablePickable(string hudName)
        {
            HeadUpName = hudName;
        }

        [XmlElement("HUDName")]
        public string HeadUpName { get; set; }
    }
}
