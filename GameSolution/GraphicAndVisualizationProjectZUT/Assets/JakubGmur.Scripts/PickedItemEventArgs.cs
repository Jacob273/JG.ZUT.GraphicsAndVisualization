namespace Assets.JakubGmur.Scripts
{
    public class PickedItemEventArgs
    {
        public PickableData pickableData { get; }
        public string playerName { get; }

        public PickedItemEventArgs(PickableData data, string name)
        {
            pickableData = data;
            playerName = name;
        }
    }
}
