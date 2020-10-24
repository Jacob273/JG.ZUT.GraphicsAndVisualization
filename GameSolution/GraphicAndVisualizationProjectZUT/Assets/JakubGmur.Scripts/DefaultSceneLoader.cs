using Assets.Global;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.JakubGmur.Scripts
{
    public class DefaultSceneLoader : MonoBehaviour
    {

        void Start()
        {
            int c = SceneManager.sceneCount;
            for (int i = 0; i < c; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                print(scene.name);
                if (scene.name != Scenes.MainApplicationViewScene)
                {
                    SceneManager.UnloadSceneAsync(scene);
                }
            }
        }

    }
}
