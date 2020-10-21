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
        public GameObject gameObjMovedByForce2;
        public GameObject gameObjThirdPartyFromAssetsStore;

        public GameObject defaultPlayer;

        private MovementController CharacterController { get { return gameObjMovedByController.GetComponentInChildren<MovementController>(); } }
        private MovementForce MovedByForce { get { return gameObjMovedByForce.GetComponentInChildren<MovementForce>(); } }
        private MovementForce MovedByForce2 { get { return gameObjMovedByForce2.GetComponentInChildren<MovementForce>(); } }
        private JGThirdPersonUserControl ThirdPersonUserControl { get { return gameObjThirdPartyFromAssetsStore.GetComponentInChildren<JGThirdPersonUserControl>(); } }
        
        private List<Camera> cameras = new List<Camera>();
        private List<IInputReceiver> inputReceivingObjects = new List<IInputReceiver>();

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
            InitializeInputReceivingObjectsList();
            TurnOnDefaultPlayer();
        }

        private void InitializeInputReceivingObjectsList()
        {
            inputReceivingObjects.Add(CharacterController);
            inputReceivingObjects.Add(MovedByForce);
            inputReceivingObjects.Add(MovedByForce2);
        }

        void TurnOnDefaultPlayer()
        {
            if (defaultPlayer == null)
            {
                TurnOnPlayerAndCamera(camera1PlayerMovedByCharacterController, CharacterController);
                return;
            }

            if(defaultPlayer.Equals(gameObjMovedByController))
            {
                TurnOnPlayerAndCamera(camera1PlayerMovedByCharacterController, CharacterController);
            }
            else if(defaultPlayer.Equals(gameObjMovedByForce))
            {
                TurnOnPlayerAndCamera(camera2PlayerMovedByForce, MovedByForce);
            }
            else if (defaultPlayer.Equals(gameObjMovedByForce2))
            {
                TurnOnPlayerAndCamera(camera3PlayerMovedByForce2, MovedByForce2);
            }
            else if (defaultPlayer.Equals(gameObjThirdPartyFromAssetsStore))
            {
                TurnOnPlayer4FromAssetsStore();
            }
        }

        void Update()
        {
            HandleKeyDown();
        }

        private void HandleKeyDown()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TurnOnPlayerAndCamera(camera1PlayerMovedByCharacterController, CharacterController);
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                TurnOnPlayerAndCamera(camera2PlayerMovedByForce, MovedByForce);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                TurnOnPlayerAndCamera(camera3PlayerMovedByForce2, MovedByForce2);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                TurnOnPlayer4FromAssetsStore();
            }
        }

        private void TurnOnPlayerAndCamera(Camera camera, MovementForce movedByForce)
        {
            DisableAllCamerasExcept(camera);
            TurnOffInputReceivingObjectsExceptOne(movedByForce);
            ThirdPersonUserControl.enabled = false;

            movedByForce.enabled = true;
            movedByForce.movingLogicShouldExecute = true;
        }

        private void TurnOnPlayerAndCamera(Camera camera, MovementController movedByCharacterController)
        {
            DisableAllCamerasExcept(camera);
            TurnOffInputReceivingObjectsExceptOne(movedByCharacterController);
            ThirdPersonUserControl.enabled = false;

            movedByCharacterController.enabled = true;
            movedByCharacterController.logicShouldExecute = true;
        }

        private void TurnOnPlayer4FromAssetsStore()
        {
            DisableAllCamerasExcept(camera4PlayerFromAssetStore);
            TurnOffInputReceivingObjectsExceptOne(null);
            ThirdPersonUserControl.enabled = true;
            MovedByForce.enabled = false;
            MovedByForce2.enabled = false;
            CharacterController.enabled = false;
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

        private void TurnOffInputReceivingObjectsExceptOne(IInputReceiver exceptionalReceiver)
        {
            foreach (var rec in inputReceivingObjects)
            {
                if (rec != exceptionalReceiver)
                {
                    rec.TurnOffInput();
                }
            }
            exceptionalReceiver?.TurnOnInput();
        }

    }
}
