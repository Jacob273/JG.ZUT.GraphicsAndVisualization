using System;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    [Serializable]
    public class PlayerObject : BaseIdentity
    {
        public new Camera camera;
        public BaseMovementScript steeringScript;
        public GameObject owner;
        public PickingItemsController pickingItemsController;
        public PlayerInventoryScript inventory;
        public ThrowingWeaponScript weapon;
        public HealthScript health;
        public SaveSpawnPointScript spawn;

        public PlayerObject() : base()
        {

        }
    }
}
