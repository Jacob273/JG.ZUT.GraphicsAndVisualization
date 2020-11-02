using Assets.Global;
using Assets.Helpers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.JakubGmur.Scripts
{
    public class HealthScript : MonoBehaviour
    {
        public Text HealthLabel;
        public float maxHealth = 105;
        private float currentHealth;

        private const float isDeadHealthValue = 0.0f;
        // Use this for initialization
        void Start()
        {
            currentHealth = maxHealth;
            UpdateLabel();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(Tags.DamageAbleTag))
            {

                var weaponDetails = other.GetComponent<WeaponDetails>();
                var attackedObject = GetComponent<BaseIdentity>();

                if ((weaponDetails.SourceId != attackedObject.Id) && !weaponDetails.WasDamaged(attackedObject.Id))
                {
                    weaponDetails.AddToDamaged(attackedObject.Id);
                    ReduceHealth(weaponDetails.Damage);
                    if(!CanLive())
                    {
                        Destroy(gameObject);
                    }
                }


            }
        }

        public void ReduceHealth(float dmg)
        {
            currentHealth -= dmg;
            UpdateLabel();
        }

        private bool CanLive()
        {
            if(DoubleComparer.Compare(currentHealth, isDeadHealthValue, 10E-2) <= 0)
            {
                return false;
            }
            return true;
        }

        public void UpdateLabel()
        {
            HealthLabel.text = currentHealth + "/" + maxHealth;
        }
    }
}
