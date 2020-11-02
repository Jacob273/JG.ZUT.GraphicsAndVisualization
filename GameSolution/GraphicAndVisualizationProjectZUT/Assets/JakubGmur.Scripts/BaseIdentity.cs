using Assets.Global;
using System;
using System.Threading;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class BaseIdentity : MonoBehaviour, IIdentity
    {
        public IdentityType type;
        public int Id { get; set; }
        public string Type { get; set; }
        public static int uniqueCounter;

        public BaseIdentity()
        {
            Interlocked.Increment(ref uniqueCounter);
            Id = uniqueCounter;
        }
    }
}
