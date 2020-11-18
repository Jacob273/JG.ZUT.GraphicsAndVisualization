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
            UpdateInventoryList(player);
        }

        [XmlElement("Position")]
        public Vector3 GlobalPosition { get; set; }

        [XmlAttribute("Id")]
        public int Id { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        public SerializableInventoryList Inventory { get; set; }


        private void UpdateInventoryList(PlayerObject player)
        {
            foreach (var inventoryElement in player.inventory.InventoryList)
            {
                var type = inventoryElement.GetType();

                if (type == typeof(PickableData))
                {
                    var pickable = inventoryElement as PickableData;
                    Inventory.List.Add(new SerializablePickable(pickable.HeadUpDisplayObj.gameObject.name));
                }
                else if (type == typeof(PickableKey))
                {
                    var key = inventoryElement as PickableKey;
                    Inventory.List.Add(new SerializablePickableKey(key.HeadUpDisplayObj.gameObject.name, key.TargetDoorId));
                }
            }
        }
    }
}
