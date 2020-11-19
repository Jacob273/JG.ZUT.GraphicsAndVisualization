using Assets.JakubGmur.XMLStructure;
using System.Linq;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class SaveSpawnPointScript : MonoBehaviour
    {
        public Vector3 LastEnteredSpawnPoint { get; set; }
        public Vector3 LastSavedSpawnPoint = Vector3.zero;
        public bool CanSaveSpawn
        {
            get => _canSaveSpawn;
            set
            {
                if (!value)
                {
                    LastEnteredSpawnPoint = Vector3.zero;
                    _canSaveSpawn = false;
                    return;
                }
                _canSaveSpawn = true;
            }
        }

        private bool _canSaveSpawn;

        public bool CanRespawn
        {
            get
            {
                if(LastSavedSpawnPoint != Vector3.zero)
                {
                    return true;
                }
                return false;
            } 
        }

        void Update()
        {

            if (Input.GetKeyDown(KeyCode.F1))
            {
                if(CanSaveSpawn)
                {
                    LastSavedSpawnPoint = LastEnteredSpawnPoint;
                    SerializeAllPlayers();
                    Messenger.Instance.UpdateMessage("Spawn point saved.");
                }
                else
                {
                    Messenger.Instance.UpdateMessage("Cannot save right now.", Color.grey);
                }
            }
            else if(Input.GetKeyDown(KeyCode.F2))
            {
                if (LastSavedSpawnPoint != Vector3.zero)
                {
                    Respawn();
                }
                else
                {
                    Messenger.Instance.UpdateMessage("Cannot respawn. No spawn points vere visited or saved.", Color.grey);
                }
            }
            else if (Input.GetKeyDown(KeyCode.F4))
            {
                PlayersXMLSerializer serializer = new PlayersXMLSerializer();
                var deserializedRoot = serializer.Deserialize();
                RestoreAllPlayersPositionAndInventory(deserializedRoot);
            }
        }

        private static void RestoreAllPlayersPositionAndInventory(XmlRoot deserializedRoot)
        {
            foreach (var player in GameObjectSwitcher.Instance.GetAllPlayers())
            {
                var deserializedPlayerData = deserializedRoot.Players.List
                                                             .Where(x => x.Id == player.Id)
                                                             .FirstOrDefault();
                player.gameObject.transform.position = deserializedPlayerData.GlobalPosition;
                BuildInventory(player, deserializedPlayerData);
            }
        }

        private static void BuildInventory(PlayerObject player, SerializablePlayer deserializedPlayerData)
        {
            if (player.inventory != null && player.inventory.InventoryList != null)
            {
                foreach (var deserializedInventoryItem in deserializedPlayerData.Inventory.List)
                {
                    var type = deserializedInventoryItem.GetType();
                    if (type == typeof(PickableData))
                    {
                        var deserializedPickable = deserializedInventoryItem as SerializablePickable;
                        player.inventory.InventoryList.Add(new PickableData(deserializedPickable.HeadUpName));
                    }
                    else if (type == typeof(PickableKey))
                    {
                        var deserializedKey = deserializedInventoryItem as SerializablePickableKey;
                        player.inventory.InventoryList.Add(new PickableKey(deserializedKey.HeadUpName, deserializedKey.TargetDoorId));
                    }
                }

            }        }

        private static void SerializeAllPlayers()
        {
            var allPlayers = GameObjectSwitcher.Instance.GetAllPlayers();
            PlayersXMLSerializer serializer = new PlayersXMLSerializer();
            serializer.Serialize(allPlayers, Debug.Log);
        }

        public bool Respawn()
        {
            if (CanRespawn)
            {
                Messenger.Instance.UpdateMessage("Respawning.........", Color.black);
                gameObject.transform.position = LastSavedSpawnPoint;
                return true;
            }
            return false;
        }
    }
}
