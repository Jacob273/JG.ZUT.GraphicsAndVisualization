using Assets.JakubGmur.Scripts;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.JakubGmur.XMLStructure
{
    [Serializable]
    public class SerializablePlayer
    {
        [XmlElement("Position")]
        public Vector3 GlobalPosition { get; set; }

        [XmlIgnore]
        public List<IInventoryItem> InventoryList { get; set; }
    }
}
