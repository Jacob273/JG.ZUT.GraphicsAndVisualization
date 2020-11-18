using Assets.JakubGmur.Scripts;
using System;
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
            Id = player.Id;
            Name = player.gameObject.name;

            Inventory = new SerializableInventoryList();
            foreach (var inventoryElement in player.inventory.InventoryList)
            {
                if(inventoryElement is PickableData pickableInventoryElement)
                {
                    Inventory.List.Add(new SerializablePickable(pickableInventoryElement.HeadUpDisplayObj.gameObject.name));
                }
                else if(inventoryElement is PickableKey key)
                {
                    Inventory.List.Add(new SerializablePickableKey(key.TargetDoorId));
                }
            }
        }

        [XmlElement("Position")]
        public Vector3 GlobalPosition { get; set; }

        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        public SerializableInventoryList Inventory { get; set; }
    }
}
