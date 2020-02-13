namespace Assets.Scripts
{
    using Assets.Enums;
    using global::AssemblyCSharp.Assets.Models;
    using UnityEngine;

    public class PlanetarySystemScaling : MonoBehaviour
    {

        public GameObject gameObjectToScale;
        public float timeAfterScalingTakesPlaceInSeconds = 6.0f;
        public float ScaleX = 1f;
        public float ScaleY = 1f;
        public float ScaleZ = 1f;

        
        private Matrix3D scalingMatrix;
        private float accumulatedTime = 0.0f;
        private Vector4 vectorToScale;
        private Vector4 scaleVector;

        // Start is called before the first frame update
        void Start()
        {
            scalingMatrix = new Matrix3D();
            if (gameObjectToScale != null)
            {
                vectorToScale = new Vector4(gameObjectToScale.transform.localScale.x, gameObjectToScale.transform.localScale.y, gameObjectToScale.transform.localScale.z, 1); ;
                scaleVector = new Vector4(ScaleX, ScaleY, ScaleZ, 1);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (gameObjectToScale != null)
            {
                accumulatedTime += Time.deltaTime;
                if (accumulatedTime > timeAfterScalingTakesPlaceInSeconds)
                {
                    scaleVector.x = ScaleX;
                    scaleVector.y = ScaleY;
                    scaleVector.z = ScaleZ;

                    scalingMatrix = Matrix3D.GenerateScalingMatrix(scalingMatrix, scaleVector);
                    vectorToScale = Matrix3D.Multiply(scalingMatrix, vectorToScale);
                    gameObjectToScale.transform.localScale = vectorToScale;
                    accumulatedTime = 0;
                }
            }
            else
            {
                Debug.Log($"gameObjectToScale is null or ScalingDirection is undefined");
            }
        }
    }
}
