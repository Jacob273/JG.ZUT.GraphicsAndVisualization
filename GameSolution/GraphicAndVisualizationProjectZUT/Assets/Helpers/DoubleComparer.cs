using System;
using System.Collections.Generic;

namespace Assets.Helpers
{
    public static class DoubleComparer
    {
        public static int Compare(double x, double y, double acceptableDeviation)
        {
            var deviation = Math.Abs(x - y);

            //If deviation is bigger than acceptableDeviation, they're not equal
            if (deviation > acceptableDeviation)
            {
                return Comparer<double>.Default.Compare(x, y);
            }
            //We consider values are equal
            return 0;
        }
    }
}
