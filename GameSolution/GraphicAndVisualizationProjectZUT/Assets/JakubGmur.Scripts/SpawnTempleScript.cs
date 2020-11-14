using Assets.Global;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class SpawnTempleScript : MonoBehaviour
    {

        private Vector3 spawnOffset = Vector3.up * 3 + Vector3.back; 

        void OnTriggerEnter(Collider other)
        {
            var otherGameObj = other.gameObject;
            if (otherGameObj.tag.Equals(Tags.PlayableTag))
            {
                var spawnScript = otherGameObj.GetComponent<PlayerObject>()?.spawn;
                if(spawnScript != null)
                {
                    Messenger.Instance.UpdateMessage("Entered save zone.");
                    var newPosition = transform.position + spawnOffset;
                    spawnScript.CanSaveSpawn = true;
                    spawnScript.LastEnteredSpawnPoint = newPosition;
                }

            }
        }

        void OnTriggerExit(Collider other)
        {
            var otherGameObj = other.gameObject;
            if (otherGameObj.tag.Equals(Tags.PlayableTag))
            {
                Messenger.Instance.UpdateMessage("You have left the save zone.");
                var spawnScript = otherGameObj.GetComponent<PlayerObject>()?.spawn;
                if(spawnScript != null)
                {
                    spawnScript.CanSaveSpawn = false;
                }
            }
        }

    }
}
