using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public interface IPointsLoader
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Vector3> GetPoints();
    }
}
