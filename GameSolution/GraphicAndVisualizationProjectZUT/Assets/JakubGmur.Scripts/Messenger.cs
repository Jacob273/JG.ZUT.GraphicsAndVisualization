using UnityEngine;
using UnityEngine.UI;

namespace Assets.JakubGmur.Scripts
{
    public class Messenger : MonoBehaviour
    {
        public static Messenger Instance = null;
        private Text _text; 


        void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            Instance._text = GetComponent<Text>();
            Instance.UpdateMessage($"Welcome to {GameDetails.GameName} ver. {GameDetails.Version}.");
        }

        public void UpdateMessage(string message)
        {
            Instance._text.text = message;
        }

    }
}
