using JakubGmur.Animations;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.JakubGmur.Scripts
{
    public class GameObjectSwitcher : MonoBehaviour
    {
        public Camera camera1;
        public Camera camera2;
        public Camera camera3;

        public GameObject gameObjMovedByController;
        public GameObject gameObjMovedByForce;
        public GameObject gameObjThirdPartyFromAssetsStore;

        public GameObject defaultPlayer;

        private MovementController characterController;
        private MovementForce movedByForce;

        List<Camera> cameras = new List<Camera>();


        void TurnOnDefaultPlayer()
        {
            Debug.Log("GameObjectSwitcher::Switching to Default player");
            if (defaultPlayer == null)
            {
                TurnOnPlayer1ControlledByCharacterController();
                return;
            }

            if(defaultPlayer.Equals(gameObjMovedByController))
            {
                Debug.Log("GameObjectSwitcher::Switching to Default 1");
                TurnOnPlayer1ControlledByCharacterController();
            }
            else if(defaultPlayer.Equals(gameObjMovedByForce))
            {
                Debug.Log("GameObjectSwitcher::Switching to Default 2");
                TurnOnPlayer2ControlledByForce();
            }
            else if (defaultPlayer.Equals(gameObjThirdPartyFromAssetsStore))
            {
                Debug.Log("GameObjectSwitcher::Switching to Default 3");
                TurnOnPlayer3FromAssetsStore();
            }
        }

        void Start()
        {
            cameras = new List<Camera>();

            if (camera1 != null)
            {
                cameras.Add(camera1);
            }

            if (camera2 != null)
            {
                cameras.Add(camera2);
            }

            if(camera3 != null)
            {
                cameras.Add(camera3);
            }

            characterController = gameObjMovedByController.GetComponentInChildren<MovementController>();
            movedByForce = gameObjMovedByForce.GetComponentInChildren<MovementForce>();

            TurnOnDefaultPlayer();
        }

        void Update()
        {
            HandleKeyDown();
        }

        private void HandleKeyDown()
        {
            TurnOnPlayer1ControlledByCharacterControllerOnKeyDown();
            TurnOnPlayer2ControlledByForceOnKeyDown();
            TurnOnPlayer3FromAssetsStoreOnKeyDown();
        }

        private void TurnOnPlayer3FromAssetsStoreOnKeyDown()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TurnOnPlayer3FromAssetsStore();
            }
        }

        private void TurnOnPlayer3FromAssetsStore()
        {
            DisableAllCamerasExcept(camera3);
            gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = true;
            movedByForce.movingLogicShouldExecute = false;
            characterController.logicShouldExecute = false;
            movedByForce.enabled = false;
            characterController.enabled = false;
        }


        private void TurnOnPlayer2ControlledByForceOnKeyDown()
        {
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                TurnOnPlayer2ControlledByForce();
            }
        }

        private void TurnOnPlayer2ControlledByForce()
        {
                DisableAllCamerasExcept(camera2);
                characterController.logicShouldExecute = false;
                gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = false;

                movedByForce.enabled = true;
                movedByForce.movingLogicShouldExecute = true;
        }

        private void TurnOnPlayer1ControlledByCharacterControllerOnKeyDown()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TurnOnPlayer1ControlledByCharacterController();
            }
        }

        private void TurnOnPlayer1ControlledByCharacterController()
        {
                DisableAllCamerasExcept(camera1);
                movedByForce.movingLogicShouldExecute = false;
                gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = false;
                characterController.enabled = true;
                characterController.logicShouldExecute = true;
        }

        private void DisableAllCamerasExcept(Camera exceptionCamera)
        {
            foreach (var camera in cameras)
            {
                if (camera != exceptionCamera)
                {
                    camera.enabled = false;
                }
                exceptionCamera.enabled = true;
            }
        }
    }
}
