using Assets.Global;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.JakubGmur.Scripts
{
    public class QuestManager : MonoBehaviour
    {
        public List<BaseIdentity> enemiesToKill;
        void Awake()
        {
            foreach(var enemy in enemiesToKill)
            {
                enemy.PublishOnDead += OnQuestEnemyDead;
            }
        }

        private void OnQuestEnemyDead(object sender, int deadId)
        {
        
            var objToRemove = enemiesToKill.Where(x => x.Id == deadId).FirstOrDefault();
            if (objToRemove != null)
            {
                enemiesToKill.Remove(objToRemove);
            }

            if(enemiesToKill.Count == 1)
            {
                Messenger.Instance.UpdateMessage("QuestInfo: You have 1 enemy left to finish the quest.");
            }

            if(enemiesToKill.Count == 0)
            {
                Messenger.Instance.UpdateMessage("QuestInfo: Congrats ! You have completed quest succesfully.");
                StartCoroutine(OnLoadNewScene());
            }
        }

        IEnumerator OnLoadNewScene()
        {
            yield return new WaitForSeconds(2.0f);
            GameSceneLoader.LoadScene(Scenes.PlayAgainViewScene);

        }
    }
}
