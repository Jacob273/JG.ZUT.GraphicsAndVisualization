using System.Collections;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class ThrowingWeaponScript : MonoBehaviour
    {
        public float throwableDistance = 850;

        public GameObject weaponToBeThrown;
        private static int counter = 1;
        private const float TimeAfterInstantiatedWeaponIsDestroyed = 5.0f;
        private const float TimeDelayBetweenThrowing = 0.5f;
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log($"Throwing {this.weaponToBeThrown.name}");
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

            instantiatedWeapon.GetComponent<WeaponDetails>().SourceId = GetComponent<PlayerObject>().Id;
            var newPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            instantiatedWeapon.transform.position = newPosition;
            instantiatedWeapon.transform.position += Vector3.up;
            var rigidBody = instantiatedWeapon.GetComponent<Rigidbody>();
            var rotation = new Quaternion(instantiatedWeapon.transform.rotation.x, instantiatedWeapon.transform.rotation.y, instantiatedWeapon.transform.rotation.z, instantiatedWeapon.transform.rotation.w);
            rigidBody.rotation = rotation;
            rigidBody.AddForce(new Vector3(x: 1, y: 1, z: 0) * throwableDistance);
            
            yield return new WaitForSeconds(TimeAfterInstantiatedWeaponIsDestroyed);
            Destroy(instantiatedWeapon);
        }
    }
}
