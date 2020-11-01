using System;
using System.Threading;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    [Serializable]
    public class PlayerObject : MonoBehaviour
    {
        public new Camera camera;
        public BaseMovementScript steeringScript;
        public GameObject owner;
        public PickingItemsController pickingItemsController;
        public PlayerInventoryScript inventory;
        public ThrowingWeaponScript weapon;
        public HealthScript health;

        public static int uniqueCounter;

        public int Id { get; }
        public PlayerObject()
        {
            Interlocked.Increment(ref uniqueCounter);
            Id = uniqueCounter;
        }
    }
}
