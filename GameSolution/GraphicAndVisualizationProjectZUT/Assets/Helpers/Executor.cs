using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Helpers
{
    public static class Executor
    {
        public static void PauseAndExecute(Action action, int delayTime)
        {
            Task.Delay(delayTime).ContinueWith((x) =>
            {
                action();
            });
        }
    }
}
