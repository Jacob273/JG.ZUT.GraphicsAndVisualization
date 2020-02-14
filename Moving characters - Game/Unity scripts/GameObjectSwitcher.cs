using System.Collections.Generic;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class GameObjectSwitcher : MonoBehaviour
    {
        public Camera camera1;
        public Camera camera2;

        public GameObject gameObjMovedByController;
        public GameObject gameObjMovedByForce;

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

            characterController = gameObjMovedByController.GetComponentInChildren<MovementController>();
            movedByForce = gameObjMovedByForce.GetComponentInChildren<MovementForce>();

            if(characterController != null)
            {
                Debug.Log("Well done!");
            }

            if(movedByForce != null)
            {
                Debug.Log("Well done!");
            }

            HandleClick1();
        }

        void Update()
        {
            HandleClick1();
            HandleClick2();
        }

        private void HandleClick2()
        {
            if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                DisableAllCamerasExcept(camera2);
                characterController.logicShouldExecute = false;

                movedByForce.enabled = true;
                movedByForce.logicShouldExecute = true;
            }
        }

        private void HandleClick1()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                DisableAllCamerasExcept(camera1);
                movedByForce.logicShouldExecute = false;

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
