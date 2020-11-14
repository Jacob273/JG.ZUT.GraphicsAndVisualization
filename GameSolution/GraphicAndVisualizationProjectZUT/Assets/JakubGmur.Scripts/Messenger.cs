using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.JakubGmur.Scripts
{
    public class Messenger : MonoBehaviour
    {
        public static Messenger Instance = null;
        private Text _text;
        private const float displayTextTimeInSecond = 2.5f;


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
            StopAllCoroutines();
            StartCoroutine(OnUpdateMessage(message));
        }

        IEnumerator OnUpdateMessage(string message)
        {
            Instance._text.text += $"{Environment.NewLine}{DateTime.Now.ToString("H:mm:ss")}: {message}";
            yield return new WaitForSeconds(displayTextTimeInSecond);
            Instance._text.text = string.Empty;
        }

    }
}
