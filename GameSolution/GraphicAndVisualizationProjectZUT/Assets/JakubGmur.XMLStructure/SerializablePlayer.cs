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

        public SerializablePlayer()
        {

        }

        public SerializablePlayer(PlayerObject player)
        {
            GlobalPosition = player.transform.position;
            InventoryList = player.inventory.InventoryList;
            Id = player.Id;
            Name = player.gameObject.name;
        }

        [XmlElement("Position")]
        public Vector3 GlobalPosition { get; set; }

        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }


        [XmlIgnore]
        public List<IInventoryItem> InventoryList { get; set; }
    }
}
