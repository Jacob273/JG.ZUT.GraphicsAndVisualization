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
                var attackedObject = GetComponent<PlayerObject>();

                if ((weaponDetails.SourceId != attackedObject.Id) && !weaponDetails.WasDamaged(attackedObject.Id))
                {
                    weaponDetails.AddToDamaged(attackedObject.Id);
                    ReduceHealth(weaponDetails.Damage);
                    if(!CanLive())
                    {
                        Messenger.Instance.UpdateMessage("You have died...");
                        if (attackedObject?.spawn != null)
                        {
                            Messenger.Instance.UpdateMessage("Trying to respawn...");
                            //TODO: respawn does not work when dieing... (?)
                            attackedObject.spawn.Respawn();
                            RestoreHealth();
                        }
                        else
                        {
                            Messenger.Instance.UpdateMessage("Game over.");
                            attackedObject.NotifyAboutDeath();
                            Destroy(gameObject);
                        }
                    }
                }
            }
        }

        public void ReduceHealth(float dmg)
        {
            currentHealth -= dmg;
            UpdateLabel();
        }

        public void RestoreHealth()
        {
            currentHealth = maxHealth;
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
