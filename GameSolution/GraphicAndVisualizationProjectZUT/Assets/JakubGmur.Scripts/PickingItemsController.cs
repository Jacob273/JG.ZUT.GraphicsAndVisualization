using Assets.Global;
using System;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class PickingItemsController : MonoBehaviour
    {

        GameObject lastEntered = null;
        PickableData lastPickedItem = null;

        public event EventHandler<GameObject> LastPickedItemChanged; 

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
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(lastEntered != null && lastPickedItem)
                {
                    Debug.Log($"PickingItemsController::LastPickedItemChanged {lastPickedItem.gameObject.name}");
                    LastPickedItemChanged.Invoke(this, lastPickedItem.HeadUpDisplayObj);
                    Destroy(lastEntered);
                    lastEntered = null;
                    lastPickedItem = null;
                }
            }
        }


    }
}
