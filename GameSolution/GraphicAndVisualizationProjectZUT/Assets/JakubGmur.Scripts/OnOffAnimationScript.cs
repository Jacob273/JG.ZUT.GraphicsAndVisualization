using System.Collections;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class OnOffAnimationScript : MonoBehaviour
    {
        public GameObject prefabWithAnimation;
        private GameObject instantiatedPrefab;

        private const float DelayBetweenNextAnimation = 1.0f;

        private int counter = 2;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if(counter % 2 == 0)
                {
                    Messenger.Instance.UpdateMessage("Super mode activated!");
                    StartCoroutine(OnStartAnimation());
                }
                else
                {
                    Messenger.Instance.UpdateMessage("Super mode deactivated.");
                    StartCoroutine(OnDestroyAnimation());
                }
                counter++;
            }
        }

        
        IEnumerator OnStartAnimation()
        {
            yield return new WaitForSeconds(DelayBetweenNextAnimation);
            var currObject = this.gameObject;
            instantiatedPrefab = Instantiate(prefabWithAnimation, currObject.transform.position, currObject.transform.rotation);
            instantiatedPrefab.transform.parent = this.gameObject.transform;
            instantiatedPrefab.transform.Translate(Vector3.up);
        }

        IEnumerator OnDestroyAnimation()
        {
            yield return new WaitForSeconds(DelayBetweenNextAnimation);
            Destroy(instantiatedPrefab);

        }
    }
}
