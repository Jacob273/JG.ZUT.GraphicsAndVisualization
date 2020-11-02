using Assets.Global;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.JakubGmur.Scripts
{
    public class GameSceneLoader : MonoBehaviour
    {
        public static void UnloadAllExcept(string nameToLoad)
        {
            int c = SceneManager.sceneCount;
            for (int i = 0; i < c; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != nameToLoad)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }

        public static void LoadScene(string sceneName)
        {
            int c = SceneManager.sceneCount;
            for (int i = 0; i < c; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name != sceneName)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
            SceneManager.LoadSceneAsync(sceneName);
        }

        void Start()
        {
            UnloadAllExcept(Scenes.MainApplicationViewScene);
        }

    }
}
