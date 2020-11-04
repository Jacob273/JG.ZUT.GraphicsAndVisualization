using System.Collections;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class ThrowingWeaponScript : MonoBehaviour
    {
        public float throwableDistance = 850;

        public GameObject weaponToBeThrown;
        public AudioSource thrownWeaponSound;
        public bool LogicShouldExecute { get; set; }

        private const float TimeAfterInstantiatedWeaponIsDestroyed = 5.0f;
        private const float TimeDelayBetweenThrowing = 0.5f;
        private float accumulatedTime = 0.0f;

        void Start()
        {
            thrownWeaponSound = GetComponent<AudioSource>();
        }

        void Update()
        {
            if(LogicShouldExecute)
            {
                accumulatedTime += Time.deltaTime;
                if (accumulatedTime >= 0.8f)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        StartCoroutine(OnThrow());
                        accumulatedTime = 0.0f;
                    }
                }
            }
        }


        IEnumerator OnThrow()
        {
            yield return new WaitForSeconds(TimeDelayBetweenThrowing);
            var currObject = gameObject;
            GameObject instantiatedWeapon = Instantiate(weaponToBeThrown, currObject.transform.position, currObject.transform.rotation);
            instantiatedWeapon.GetComponent<WeaponDetails>().SourceId = GetComponent<PlayerObject>().Id;
            var rigidBody = instantiatedWeapon.GetComponent<Rigidbody>();
            rigidBody.AddRelativeForce(Vector3.forward * throwableDistance);


            thrownWeaponSound?.Play();

            yield return new WaitForSeconds(TimeAfterInstantiatedWeaponIsDestroyed);
            Destroy(instantiatedWeapon);
        }
    }
}
