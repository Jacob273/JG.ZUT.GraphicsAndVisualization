using Assets.Global;
using System.Linq;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class SlidingDoorKeyRequiredScript : SlidingDoorScript
    {
        public string DoorId;

        protected override void HandleEnter(Collider other)
        {
            if (other.CompareTag(Tags.PlayableTag))
            {
                var player = other.gameObject.GetComponent<PlayerObject>();
                if (player != null)
                {
                    var hasKey = player.inventory.InventoryList
                                                 .OfType<PickableKey>()
                                                 .Any(x => x.TargetDoorId == this.DoorId);
                    if (hasKey)
                    {
                        _currentExecutionLoggic = Execution.StartOpening;
                    }
                }
            }
        }
    }
}
