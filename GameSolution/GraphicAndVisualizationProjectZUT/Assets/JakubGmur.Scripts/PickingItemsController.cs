using Assets.Global;
using System;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class PickingItemsController : MonoBehaviour
    {

        GameObject lastEntered = null;
        PickableData lastPickedItem = null;
        

        public event EventHandler<PickedItemEventArgs> LastPickedItemChanged; 
        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag.Equals(Tags.PickableTag))
            {
                var pickableItem = other.gameObject.GetComponent<PickableData>();
                if (pickableItem != null)
                {
                    Debug.Log($"PickingItemsController::Found pickable!!!!!");
                    lastEntered = other.gameObject;
                    lastPickedItem = pickableItem;
                }
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if(lastEntered != null && lastPickedItem)
                {
                    LastPickedItemChanged.Invoke(this, new PickedItemEventArgs(lastPickedItem, gameObject.name));
                    Destroy(lastEntered);
                    Messenger.Instance.UpdateMessage($"Picked {lastPickedItem.name}");
                    lastEntered = null;
                    lastPickedItem = null;
                }
            }
        }


    }
}
