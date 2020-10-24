using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class GameObjectSwitcher : MonoBehaviour
    {
        private const int Player1Index = 0;
        private const int Player2Index = 1;
        private const int Player3Index = 2;
        private const int Player4Index = 3;

        public int defaultPlayerIndex = 0;
        public event EventHandler OnMainPlayerChanged;

        public List<GameObject> playersObjects;

        void Start()
        {
            TurnOnDefaultPlayer();
        }

        void TurnOnDefaultPlayer()
        {
            TurnOnPlayerAndCamera(playersObjects[defaultPlayerIndex]);
            return;
        }

        void Update()
        {
            HandleKeyDown();
        }

        private void HandleKeyDown()
        {
            int? playerIndex = null;

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                playerIndex = Player1Index;
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                playerIndex = Player2Index;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                playerIndex = Player3Index;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                playerIndex = Player4Index;
            }

            if(playerIndex.HasValue)
            {
                TurnOnPlayerAndCamera(playersObjects[playerIndex.Value]);
            }
        }

        private void TurnOnPlayerAndCamera(GameObject playerObject)
        {
            var player = playerObject.GetComponentInChildren<PlayerObject>();
            DisableAllCamerasExcept(player.camera);
            TurnOffInputReceivingObjectsExceptOne(player.steeringScript);
            player.steeringScript.Enable();
            player.steeringScript.TurnOnInput();
        }

        private void DisableAllCamerasExcept(Camera activeCamera)
        {
            foreach (var playerObj in playersObjects)
            {
                var player = playerObj.GetComponentInChildren<PlayerObject>();
                if (activeCamera != player.camera)
                {
                    player.camera.enabled = false;
                }
            }
            activeCamera.enabled = true;
        }

        private void TurnOffInputReceivingObjectsExceptOne(BaseMovementScript activeScript)
        {
            foreach (var playerObj in playersObjects)
            {
                var player = playerObj.GetComponentInChildren<PlayerObject>();
                if (player.steeringScript != activeScript)
                {
                    player.steeringScript.TurnOffInput();
                }
            }
            activeScript.TurnOnInput();
        }
    }
}
