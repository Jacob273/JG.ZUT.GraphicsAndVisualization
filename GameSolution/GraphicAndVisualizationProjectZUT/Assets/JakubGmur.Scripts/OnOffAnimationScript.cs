using System.Collections;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class OnOffAnimationScript : MonoBehaviour
    {
        public GameObject prefabWithAnimation;
        private GameObject instantiatedPrefab;

        private static int counter = 1;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                counter++;
                if(counter % 2 ==0)
                {
                    StartCoroutine(OnStartAnimation());
                }
                else
                {
                    Destroy(instantiatedPrefab);
                }
            }
        }


        IEnumerator OnStartAnimation()
        {
            yield return new WaitForSeconds(1);
            var currObject = this.gameObject;
            instantiatedPrefab = Instantiate(prefabWithAnimation, currObject.transform.position, currObject.transform.rotation);
            instantiatedPrefab.transform.parent = this.gameObject.transform;
            instantiatedPrefab.transform.Translate(Vector3.up);
        }
    }
}
