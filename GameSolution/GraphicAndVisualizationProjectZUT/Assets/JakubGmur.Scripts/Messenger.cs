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
        private static Color DefaultTextColor = Color.black;

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
            Instance._text.supportRichText = true;
            Instance.UpdateMessage($"Welcome to {GameDetails.GameName} ver. {GameDetails.Version}.");
        }

        private void AppendColor(ref string message, Color color)
        {
            Debug.Log($"${ColorUtility.ToHtmlStringRGB(color)}");
            message = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{message}</color>";
            Debug.Log(message);
        }

        public void UpdateMessage(string message)
        {
            AppendColor(ref message, DefaultTextColor);
            StopAllCoroutines();
            StartCoroutine(OnUpdateMessage(message));
        }

        public void UpdateMessage(string message, Color color)
        {
            AppendColor(ref message, color);
            StopAllCoroutines();
            StartCoroutine(OnUpdateMessage(message));
        }

        private IEnumerator OnUpdateMessage(string messageIncludingColor)
        {
            Instance._text.text += $"{Environment.NewLine}{DateTime.Now.ToString("H:mm:ss")}: {messageIncludingColor}";
            yield return new WaitForSeconds(displayTextTimeInSecond);
            Instance._text.text = string.Empty;
        }
    }
}
