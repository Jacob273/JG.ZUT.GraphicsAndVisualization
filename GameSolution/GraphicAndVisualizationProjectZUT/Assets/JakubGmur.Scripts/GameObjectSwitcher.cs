﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class GameObjectSwitcher : MonoBehaviour
    {
        public static GameObjectSwitcher Instance = null;

        private const int Player1Index = 0;
        private const int Player2Index = 1;
        private const int Player3Index = 2;
        private const int Player4Index = 3;

        public int defaultPlayerIndex = 0;
        public event EventHandler<PlayerObject> OnMainPlayerChanged;

        public List<GameObject> playersObjects;

        public List<PlayerObject> GetAllPlayers()
        {
            var players = new List<PlayerObject>();
            foreach(var playerGameObj in playersObjects)
            {
                players.Add(playerGameObj.GetComponentInChildren<PlayerObject>());
            }
            return players;
        }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            TurnOnDefaultPlayer();
        }

        void TurnOnDefaultPlayer()
        {
            var activePlayer = playersObjects[defaultPlayerIndex];
            TurnOnPlayerAndCamera(activePlayer);
            NotifyNewPlayerChanged(activePlayer);
        }

        private void NotifyNewPlayerChanged(GameObject activePlayer)
        {
            var acivePlayerObj = activePlayer.GetComponentInChildren<PlayerObject>();
            OnMainPlayerChanged?.Invoke(this, acivePlayerObj);
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
                var newActivePlayer = playersObjects[playerIndex.Value];
                TurnOnPlayerAndCamera(newActivePlayer);
            }
        }

        private void TurnOnPlayerAndCamera(GameObject playerObject)
        {
            var newActivePlayer = playerObject.GetComponentInChildren<PlayerObject>();
            if (newActivePlayer != null)
            {
                DisableAllCamerasExcept(newActivePlayer.camera);
                TurnOffInputReceivingObjectsExceptOne(newActivePlayer.steeringScript);
                TurnOffThrowingExceptOne(newActivePlayer.weapon);
                newActivePlayer.steeringScript.Enable();
                newActivePlayer.steeringScript.TurnOnInput();
                OnMainPlayerChanged?.Invoke(this, newActivePlayer);
            }
        }

        private void DisableAllCamerasExcept(Camera activeCamera)
        {
            foreach (var playerObj in playersObjects)
            {
                var player = playerObj.GetComponentInChildren<PlayerObject>();
                if(player != null)
                {
                    if (activeCamera != player.camera)
                    {
                        player.camera.enabled = false;
                        player.camera.GetComponent<AudioListener>().enabled = false;
                    }
                }
            }
            activeCamera.enabled = true;
            activeCamera.GetComponent<AudioListener>().enabled = true;
        }

        private void TurnOffInputReceivingObjectsExceptOne(BaseMovementScript activeScript)
        {
            foreach (var playerObj in playersObjects)
            {
                var player = playerObj.GetComponentInChildren<PlayerObject>();
                if (player.steeringScript != null && player.steeringScript != activeScript)
                {
                    player.steeringScript.TurnOffInput();
                }
            }

            if (activeScript != null)
            {
                activeScript.TurnOnInput();
            }
        }

        private void TurnOffThrowingExceptOne(ThrowingWeaponScript activeScript)
        {
            foreach (var playerObj in playersObjects)
            {
                var player = playerObj.GetComponentInChildren<PlayerObject>();
                if (player.weapon != null && player.weapon != activeScript)
                {
                    player.weapon.LogicShouldExecute = false;
                }
            }

            if (activeScript != null)
            {
                activeScript.LogicShouldExecute = true;
            }
        }

        public Camera GetActiveCamera()
        {
            return playersObjects.Select(x => x.GetComponentInChildren<PlayerObject>()?.camera)
                                 .Where(x => x?.enabled == true).FirstOrDefault();
        }
    }
}
