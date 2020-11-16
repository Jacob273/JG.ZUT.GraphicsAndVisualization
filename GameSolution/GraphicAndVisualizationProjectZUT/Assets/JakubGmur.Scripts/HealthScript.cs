using Assets.Global;
using Assets.Helpers;
using System.Collections;
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
                        Messenger.Instance.UpdateMessage($"Player <{attackedObject.Id}> have died...");
                        StopMovementImmediately(attackedObject);
                        if (attackedObject?.spawn != null && attackedObject.spawn.Respawn())
                        {
                            RestoreHealth();
                            StartCoroutine(StartMovementWithDelay(attackedObject));
                        }
                        else
                        {
                            Died(attackedObject);
                        }
                    }
                }
            }
        }

        const float disabledTime = 1.5f;

        IEnumerator StartMovementWithDelay(PlayerObject respawnedObject)
        {
            yield return new WaitForSeconds(disabledTime);
            respawnedObject.steeringScript.Enable();
            respawnedObject.steeringScript.TurnOnInput();
            Messenger.Instance.UpdateMessage($"Player <{respawnedObject.Id}> is not exhausted anymore!");
        }


        void StopMovementImmediately(PlayerObject deadObject)
        {
            Messenger.Instance.UpdateMessage($"Player <{deadObject.Id}> is exhausted...");
            deadObject.steeringScript.Disable();
            deadObject.steeringScript.TurnOffInput();
        }

        private void Died(PlayerObject attackedObject)
        {
            Messenger.Instance.UpdateMessage($"Player <{attackedObject.Id}> has ended his game.");
            attackedObject.NotifyAboutDeath();
            Destroy(gameObject);
        }

        public void ReduceHealth(float dmg)
        {
            Messenger.Instance.UpdateMessage($"DMG: -{dmg}");
            currentHealth -= dmg;
            UpdateLabel();
        }

        public void RestoreHealth()
        {
            currentHealth = maxHealth;
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
