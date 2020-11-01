using System.Collections;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class ThrowableWeaponScript : MonoBehaviour
    {
        public GameObject weaponToBeThrown;
        public float throwableDistance = 850;

        private static int counter = 1;
        private const float TimeAfterInstantiatedWeaponIsDestroyed = 5.0f;
        private const float TimeDelayBetweenThrowing = 0.5f;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                counter++;
                if (counter % 2 == 0)
                {
                    StartCoroutine(OnThrow());
                }
            }
        }

        
        IEnumerator OnThrow()
        {
            yield return new WaitForSeconds(TimeDelayBetweenThrowing);
            var currObject = gameObject;
            GameObject instantiatedWeapon = Instantiate(weaponToBeThrown, currObject.transform.position, currObject.transform.rotation);
            instantiatedWeapon.transform.parent = gameObject.transform;
            instantiatedWeapon.transform.Translate(Vector3.up);
            var rigidBody = instantiatedWeapon.GetComponent<Rigidbody>();
            rigidBody.AddForce(new Vector3(x: 1, y: 1, z: 0) * throwableDistance);
            
            yield return new WaitForSeconds(TimeAfterInstantiatedWeaponIsDestroyed);
            Destroy(instantiatedWeapon);
        }
    }
}
