using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class RotateHead : MonoBehaviour
    {
        private float rotationCounter = 38;
        Pressed currentState = Pressed.Noone;


        public enum Pressed
        {
            Noone,
            UpPressed,
            UpReleased,
            DownPressed,
            DownReleased,
        }


        void Update()
        {
            //Check the current state...
            GetCurrentState();

            switch (currentState)
            {
                case Pressed.DownPressed:
                    transform.localRotation = Quaternion.Euler(new Vector3(rotationCounter--, transform.localRotation.y, transform.localRotation.z));
                    break;

                case Pressed.DownReleased:
                case Pressed.UpReleased:
                    transform.localRotation = Quaternion.Euler(new Vector3(rotationCounter, transform.localRotation.y, transform.localRotation.z));
                    break;

                case Pressed.UpPressed:
                    transform.localRotation = Quaternion.Euler(new Vector3(rotationCounter++, transform.localRotation.y, transform.localRotation.z));
                    break;

                case Pressed.Noone:
                    //Debug.Log("rotationCounter::" + rotationCounter);
                    break;
            }

        }

        private void GetCurrentState()
        {
            if (Input.GetKeyUp(KeyCode.Q))
            {
                currentState = Pressed.UpReleased;
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                currentState = Pressed.UpPressed;
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                currentState = Pressed.DownReleased;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                currentState = Pressed.DownPressed;
            }
        }
    }
}
