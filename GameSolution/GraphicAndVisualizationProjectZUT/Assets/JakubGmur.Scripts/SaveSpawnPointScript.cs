using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class SaveSpawnPointScript : MonoBehaviour
    {
        public Vector3 LastEnteredSpawnPoint { get; set; }
        public Vector3 LastSavedSpawnPoint = Vector3.zero;
        public bool CanSaveSpawn
        {
            get => _canSaveSpawn;
            set
            {
                if (!value)
                {
                    LastEnteredSpawnPoint = Vector3.zero;
                    _canSaveSpawn = false;
                    return;
                }
                _canSaveSpawn = true;
            }
        }

        private bool _canSaveSpawn;

        public bool CanRespawn
        {
            get
            {
                if(LastSavedSpawnPoint != Vector3.zero)
                {
                    return true;
                }
                return false;
            } 
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.F1))
            {
                if(CanSaveSpawn)
                {
                    LastSavedSpawnPoint = LastEnteredSpawnPoint;
                    Messenger.Instance.UpdateMessage("Spawn point saved.");
                }
                else
                {
                    Messenger.Instance.UpdateMessage("Cannot save right now.");
                }
            }
            else if(Input.GetKeyDown(KeyCode.F2))
            {
                if (LastSavedSpawnPoint != Vector3.zero)
                {
                    Respawn();
                }
                else
                {
                    Messenger.Instance.UpdateMessage("Cannot respawn. No spawn points vere visited or saved.");
                }
            }
        }

        public void Respawn()
        {
            if(CanRespawn)
            {
                Messenger.Instance.UpdateMessage("Respawning.........");
                gameObject.transform.position = LastSavedSpawnPoint;
                Debug.Log(LastSavedSpawnPoint);
            }
        }
    }
}
