using TMPro;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class HealthScript : MonoBehaviour
    {
        public TMP_Text HealthTextOverHead;
        public float maxHealth = 100;
        public float currentHealth;

        // Use this for initialization
        void Start()
        {
            currentHealth = maxHealth;
            HealthTextOverHead.text = currentHealth + "/" + maxHealth;
        }
    }
}
