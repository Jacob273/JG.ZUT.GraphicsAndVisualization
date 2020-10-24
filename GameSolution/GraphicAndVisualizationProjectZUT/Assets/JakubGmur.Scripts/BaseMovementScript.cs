using System;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public abstract class BaseMovementScript : MonoBehaviour
    {
        public abstract void Disable();
        public abstract void Enable();
        public abstract void TurnOffInput();
        public abstract void TurnOnInput();
    }
}
