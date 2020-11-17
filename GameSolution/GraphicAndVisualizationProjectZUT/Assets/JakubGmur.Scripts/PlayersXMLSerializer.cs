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

        public void Serialize(PlayerObject player, Action<string> onSerializationInfoCallback)
        {
            SerializablePlayer playerToSerialize = new SerializablePlayer();
            playerToSerialize.GlobalPosition = player.transform.position;
            playerToSerialize.InventoryList = player.inventory.InventoryList;

            string fullFilePath = $"{CurrentDir}/{fileName}";
            try
            {
                TextWriter writer = new StreamWriter(fullFilePath);
                XmlSerializer serializer = new XmlSerializer(typeof(SerializablePlayer));
                serializer.Serialize(writer, playerToSerialize);
                onSerializationInfoCallback($"Saved file on path {fullFilePath}");
            }
            catch(Exception ex)
            {
                onSerializationInfoCallback($"Could not save file on path <{fullFilePath}> reason <{ex.Message}>");
            }
        }
    }
}
