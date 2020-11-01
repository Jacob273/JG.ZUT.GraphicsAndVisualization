using System.Collections.Generic;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class WeaponDetails : MonoBehaviour
    {
        public int SourceId { get; set; }
        public float Damage = 25.0f;
        private HashSet<int> DamagedPlayers { get; } = new HashSet<int>(); 

        public void AddToDamaged(int playerId)
        {
            if (playerId != SourceId)
            {
                if (!DamagedPlayers.Contains(playerId))
                {
                    DamagedPlayers.Add(playerId);
                }
            }
        }

        public bool WasDamaged(int playerId)
        {
            return DamagedPlayers.Contains(playerId);
        }
    }
}
