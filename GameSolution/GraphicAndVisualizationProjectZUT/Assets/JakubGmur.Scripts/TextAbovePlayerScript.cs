using UnityEngine;
using UnityEngine.UI;

namespace Assets.JakubGmur.Scripts
{
    public class TextAbovePlayerScript : MonoBehaviour
    {
        private Text _textObj;
        private GameObjectSwitcher _gameObjSwitcher;

        void Awake()
        {
            _gameObjSwitcher = FindObjectOfType<GameObjectSwitcher>();
            _textObj = GetComponentInChildren<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            var activeCamera = _gameObjSwitcher.GetActiveCamera();
            if (activeCamera != null)
            {
                Vector3 targetPosition = activeCamera.WorldToScreenPoint(gameObject.GetComponentInParent<PlayerObject>().transform.position);
                _textObj.transform.position = targetPosition;
            }
        }
    }
}
