using Assets.JakubGmur.XMLStructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.JakubGmur.Scripts
{
    public class PlayersXMLSerializer
    {
        private static string CurrentDir = Directory.GetCurrentDirectory();
        private const string fileName = "UnityAdventureXamePlayerObjects.xml";
        private static string FullFilePath = $"{CurrentDir}/{fileName}";

        public void Serialize(List<PlayerObject> players, Action<string> onSerializationInfoCallback)
        {
            XmlRoot xmlRoot = new XmlRoot();
            
            foreach (var player in players)
            {
                xmlRoot.Players.Add(new SerializablePlayer(player));
            }

            try
            {
                using (TextWriter writer = new StreamWriter(FullFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(XmlRoot));
                    serializer.Serialize(writer, xmlRoot);
                    onSerializationInfoCallback($"Saved file on path {FullFilePath}");
                }
            }
            catch (Exception ex)
            {
                onSerializationInfoCallback($"Could not save file on path <{FullFilePath}> reason <{ex.Message}>");
            }
        }

        public XmlRoot Deserialize()
        {
            if (!File.Exists(FullFilePath))
            {
                return null;
            }
            else
            {
                using (FileStream stream = new FileStream(FullFilePath, FileMode.Open))
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof(XmlRoot));
                    return deserializer.Deserialize(stream) as XmlRoot;
                }
            }
        }
    }
}
