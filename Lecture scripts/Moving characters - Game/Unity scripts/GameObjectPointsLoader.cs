using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class GameObjectPointsLoader : MonoBehaviour, IPointsLoader
    {
        public List<GameObject> points;

        public List<Vector3> GetPoints()
        {
            return points.Select(item => new Vector3()
            {
                x = item.transform.position.x,
                y = item.transform.position.y,
                z = item.transform.position.z,
            }).ToList();
        }
    }
}
