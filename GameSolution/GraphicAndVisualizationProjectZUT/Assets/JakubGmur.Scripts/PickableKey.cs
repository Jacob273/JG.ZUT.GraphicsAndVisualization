namespace Assets.JakubGmur.Scripts
{
    public class PickableKey : PickableData, IInventoryItem
    {
        public string TargetDoorId;

        public PickableKey(string hudName, string targetDoorId): base(hudName)
        {
            TargetDoorId = targetDoorId;
        }
    }
}
