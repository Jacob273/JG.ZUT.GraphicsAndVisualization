namespace JakubGmur.Unity.AssemblyCSharp.Assets.Scripts
{
    using global::AssemblyCSharp.Assets.Models;
    using global::Assets.Enums;
    using System;
    using UnityEngine;

    public class PlanetarySystemTranslation : MonoBehaviour
    {
        public GameObject gameObjectToMove;

        public float MovementSpeedX = 0.5f;
        public float MovementSpeedY = 0.5f;
        public float MovementSpeedZ = 0.5f;

        private Vector4 offsetVector;
        private Vector4 vectorToTranslate;
        private Matrix3D translationMatrix;

        // Start is called before the first frame update
        void Start()
        {
            translationMatrix = new Matrix3D();
            if (gameObjectToMove != null)
            {
                offsetVector = new Vector4(0, 0, 0, 1); ;
                vectorToTranslate = new Vector4(gameObjectToMove.transform.position.x, gameObjectToMove.transform.position.y, gameObjectToMove.transform.position.z, 1);
            }
        }   

        // Update is called once per frame
        void Update()
        {
            if (gameObjectToMove != null &&
                vectorToTranslate != null)
            {
                offsetVector.x = MovementSpeedX * Time.deltaTime;
                offsetVector.y = MovementSpeedY * Time.deltaTime;
                offsetVector.z = MovementSpeedZ * Time.deltaTime;

                translationMatrix = Matrix3D.GenerateTranslationMatrix(translationMatrix, offsetVector);
                vectorToTranslate = Matrix3D.Multiply(translationMatrix, vectorToTranslate);
                gameObjectToMove.transform.position = vectorToTranslate;
            }
            else
            {
                Debug.Log($"gameObjectToMove is null");
            }
        }
    }
}
