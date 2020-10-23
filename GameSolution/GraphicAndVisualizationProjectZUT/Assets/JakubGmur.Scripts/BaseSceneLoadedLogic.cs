using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.JakubGmur.Scripts
{
    public abstract class BaseSceneLoadedLogic : MonoBehaviour
    {
        public List<GameObject> objects;

        public abstract void ExecuteLogic(GameObject obj);

        public void ExecuteForAll()
        {
            if (objects != null)
            {
                foreach (var obj in objects)
                {
                    ExecuteLogic(obj);
                }
            }
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ExecuteForAll();
        }
    }
}
