using System;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    [Serializable]
    public class PlayerObject : MonoBehaviour
    {
        public new Camera camera;
        public BaseMovementScript steeringScript;
        public GameObject owner;
        public MonoBehaviour pickingItemsController;
    }
}
