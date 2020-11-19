using System.Linq;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class PickableData : MonoBehaviour, IInventoryItem
    {
        public GameObject HeadUpDisplayObj;

        public PickableData(string hudName)
        {
            GetHudFromController(hudName);
        }

        private void GetHudFromController(string hudName)
        {
            HeadUpDisplayObj = HeadUpDisplayController.Instance.hudImages
                                                               .Where(x => x.name == hudName)
                                                               .FirstOrDefault();
        }
    }
}