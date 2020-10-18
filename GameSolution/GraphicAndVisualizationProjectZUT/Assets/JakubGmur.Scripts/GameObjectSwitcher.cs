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

        private MovementController characterController;
        private MovementForce movedByForce;

        List<Camera> cameras = new List<Camera>();

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

            TurnOnPlayer3FromAssetsStore();
        }

        void Update()
        {
            TurnOnPlayer1ControlledByCharacterController();
            TurnOnPlayer2ControlledByForce();
            TurnOnPlayer3FromAssetsStore();
        }

        private void TurnOnPlayer3FromAssetsStore()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                DisableAllCamerasExcept(camera3);
                gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = true;
                movedByForce.movingLogicShouldExecute = false;
                characterController.logicShouldExecute = false;
                movedByForce.enabled = false;
                characterController.enabled = false;
            }
        }

        private void TurnOnPlayer2ControlledByForce()
        {
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                DisableAllCamerasExcept(camera2);
                characterController.logicShouldExecute = false;
                gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = false;

                movedByForce.enabled = true;
                movedByForce.movingLogicShouldExecute = true;
            }
        }

        private void TurnOnPlayer1ControlledByCharacterController()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DisableAllCamerasExcept(camera1);
                movedByForce.movingLogicShouldExecute = false;
                gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>().enabled = false;

                characterController.enabled = true;
                characterController.logicShouldExecute = true;
            }
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
