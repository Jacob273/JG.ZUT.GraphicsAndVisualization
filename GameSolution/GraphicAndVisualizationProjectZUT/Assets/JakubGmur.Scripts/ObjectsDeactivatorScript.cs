using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class ObjectsDeactivatorScript : BaseSceneLoadedLogic
    {
        public override void ExecuteLogic(GameObject obj)
        {
            obj.SetActive(false);
        }
    }
}
