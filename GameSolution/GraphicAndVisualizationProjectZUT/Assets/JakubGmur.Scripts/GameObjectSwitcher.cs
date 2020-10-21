using JakubGmur.Animations;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class GameObjectSwitcher : MonoBehaviour
    {
        public Camera camera1PlayerMovedByCharacterController;
        public Camera camera2PlayerMovedByForce;
        public Camera camera3PlayerMovedByForce2;
        public Camera camera4PlayerFromAssetStore;

        public GameObject gameObjMovedByController;
        public GameObject gameObjMovedByForce;
        public GameObject gameObjThirdPartyFromAssetsStore;
        public GameObject gameObjMovedByForce2;

        public GameObject defaultPlayer;

        private MovementController characterController;
        private MovementForce movedByForce; 
        private MovementForce movedByForce2;
        private List<Camera> cameras = new List<Camera>();


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
                TurnOnPlayer1ControlledByCharacterController();
            }
            else if(defaultPlayer.Equals(gameObjMovedByForce))
            {
                TurnOnPlayer2ControlledByForce();
            }
            else if (defaultPlayer.Equals(gameObjMovedByForce2))
            {
                TurnOnPlayer3ControlledByForce();
            }
            else if (defaultPlayer.Equals(gameObjThirdPartyFromAssetsStore))
            {
                TurnOnPlayer4FromAssetsStore();
            }
        }

        void Start()
        {
            cameras = new List<Camera>();

            if (camera1PlayerMovedByCharacterController != null)
            {
                cameras.Add(camera1PlayerMovedByCharacterController);
            }

            if (camera2PlayerMovedByForce != null)
            {
                cameras.Add(camera2PlayerMovedByForce);
            }

            if (camera3PlayerMovedByForce2 != null)
            {
                cameras.Add(camera3PlayerMovedByForce2);
            }

            if (camera4PlayerFromAssetStore != null)
            {
                cameras.Add(camera4PlayerFromAssetStore);
            }

            characterController = gameObjMovedByController.GetComponentInChildren<MovementController>();
            movedByForce = gameObjMovedByForce.GetComponentInChildren<MovementForce>();
            movedByForce2 = gameObjMovedByForce2.GetComponentInChildren<MovementForce>();
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
            TurnOnPlayer3ControlledByForceOnKeyDown();
            TurnOnPlayer4FromAssetsStoreOnKeyDown();
        }

        private void TurnOnPlayer1ControlledByCharacterController()
        {
            DisableAllCamerasExcept(camera1PlayerMovedByCharacterController);
            movedByForce.movingLogicShouldExecute = false;
            movedByForce2.movingLogicShouldExecute = false;
            gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = false;

            characterController.enabled = true;
            characterController.logicShouldExecute = true;
        }

        private void TurnOnPlayer2ControlledByForce()
        {
            DisableAllCamerasExcept(camera2PlayerMovedByForce);
            characterController.logicShouldExecute = false;
            movedByForce2.movingLogicShouldExecute = false;
            gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = false;

            movedByForce.enabled = true;
            movedByForce.movingLogicShouldExecute = true;
        }

        private void TurnOnPlayer3ControlledByForce()
        {
            DisableAllCamerasExcept(camera3PlayerMovedByForce2);
            characterController.logicShouldExecute = false;
            movedByForce.movingLogicShouldExecute = false;
            gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = false;

            movedByForce2.enabled = true;
            movedByForce2.movingLogicShouldExecute = true;
        }

        private void TurnOnPlayer4FromAssetsStore()
        {
            DisableAllCamerasExcept(camera4PlayerFromAssetStore);
            gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = true;
            movedByForce.movingLogicShouldExecute = false;
            movedByForce2.movingLogicShouldExecute = false;
            characterController.logicShouldExecute = false;
            movedByForce.enabled = false;
            movedByForce2.enabled = false;
            characterController.enabled = false;
        }

        #region OnKeyDown

        private void TurnOnPlayer1ControlledByCharacterControllerOnKeyDown()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TurnOnPlayer1ControlledByCharacterController();
            }
        }

        private void TurnOnPlayer2ControlledByForceOnKeyDown()
        {
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                TurnOnPlayer2ControlledByForce();
            }
        }

        private void TurnOnPlayer3ControlledByForceOnKeyDown()
        {
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                TurnOnPlayer3ControlledByForce();
            }
        }

        private void TurnOnPlayer4FromAssetsStoreOnKeyDown()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                TurnOnPlayer4FromAssetsStore();
            }
        }

        #endregion

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
