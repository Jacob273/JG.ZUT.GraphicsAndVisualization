using UnityEngine;
using UnityEngine.UI;

namespace Assets.JakubGmur.Scripts
{
    public class TextAbovePlayerScript : MonoBehaviour
    {
        public Text _textObjForName;
        public Text _textObjForHealth;
        private GameObjectSwitcher _gameObjSwitcher;

        void Awake()
        {
            _gameObjSwitcher = FindObjectOfType<GameObjectSwitcher>();
        }

        // Update is called once per frame
        void Update()
        {
            var activeCamera = _gameObjSwitcher.GetActiveCamera();
            if (activeCamera != null)
            {
                Vector3 targetPosition = activeCamera.WorldToScreenPoint(gameObject.GetComponentInParent<PlayerObject>().transform.position);
                var forNamePosition = targetPosition;
                _textObjForName.transform.position = forNamePosition;

                const int yOffset = 35;
                var forHealthPosition = forNamePosition + Vector3.down * yOffset;
                _textObjForHealth.transform.position = forHealthPosition;
            }
        }
    }
}
