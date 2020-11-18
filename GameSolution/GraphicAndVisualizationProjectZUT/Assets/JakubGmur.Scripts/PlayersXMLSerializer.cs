using Assets.JakubGmur.XMLStructure;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Assets.JakubGmur.Scripts
{
    public class PlayersXMLSerializer
    {
        private static string CurrentDir = Directory.GetCurrentDirectory();
        private const string fileName = "UnityAdventureXamePlayerObjects.xml";
        private static string FullFilePath = $"{CurrentDir}/{fileName}";

        public void Serialize(PlayerObject player, Action<string> onSerializationInfoCallback)
        {
            SerializablePlayer playerToSerialize = new SerializablePlayer();
            playerToSerialize.GlobalPosition = player.transform.position;
            playerToSerialize.InventoryList = player.inventory.InventoryList;

            try
            {
                using (TextWriter writer = new StreamWriter(FullFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(SerializablePlayer));
                    serializer.Serialize(writer, playerToSerialize);
                    onSerializationInfoCallback($"Saved file on path {FullFilePath}");
                }
            }
            catch (Exception ex)
            {
                onSerializationInfoCallback($"Could not save file on path <{FullFilePath}> reason <{ex.Message}>");
            }
        }

        public SerializablePlayer Deserialize()
        {
            if (!File.Exists(FullFilePath))
            {
                return null;
            }
            else
            {
                using (FileStream stream = new FileStream(FullFilePath, FileMode.Open))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(SerializablePlayer));
                    return deserializer.Deserialize(stream) as SerializablePlayer;
                }
            }
        }
    }
}
