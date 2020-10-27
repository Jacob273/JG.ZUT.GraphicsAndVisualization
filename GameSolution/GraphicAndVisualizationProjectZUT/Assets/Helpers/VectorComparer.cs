using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Helpers
{
    public static class VectorComparer
    {
        public static bool AreEqual(Vector3 a, Vector3 b, float precision = 0.01f)
        {
            return Vector3.SqrMagnitude(a - b) < precision;
        }
    }
}
