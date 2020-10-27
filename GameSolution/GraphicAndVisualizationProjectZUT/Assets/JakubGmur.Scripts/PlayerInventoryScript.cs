using System.Collections.Generic;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class PlayerInventoryScript : MonoBehaviour
    {
        public PlayerObject inventoryOwner;
        public List<IInventoryItem> InventoryList { get; set; }

        void Start()
        {
            InventoryList = new List<IInventoryItem>();
            inventoryOwner.pickingItemsController.LastPickedItemChanged += OnLastPickedItemChanged;
        }

        private void OnLastPickedItemChanged(object sender, PickedItemEventArgs e)
        {
            InventoryList.Add(e.pickableData);
            Debug.Log($"{InventoryList.Count}");
        }
    }
}
