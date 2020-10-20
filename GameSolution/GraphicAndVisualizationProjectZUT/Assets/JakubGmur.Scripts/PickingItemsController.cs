using Assets.Global;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class PickingItemsController : MonoBehaviour
    {

        GameObject lastEntered = null;

        void OnTriggerEnter(Collider other)
        {
            Debug.Log($"Entered {other.name}");

            if (other.gameObject.tag.Equals(Tags.PickableTag))
            {
                Debug.Log($"Found pickable!!!!!");
                lastEntered = other.gameObject;
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if(lastEntered != null)
                {
                    Destroy(lastEntered);
                    lastEntered = null;
                }
            }
        }


    }
}
