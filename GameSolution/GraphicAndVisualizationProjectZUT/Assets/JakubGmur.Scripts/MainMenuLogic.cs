using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject AboutProgramMenu;

    public string SceneToLoad;

    void Start()
    {
        ShowMainMenu();
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(SceneToLoad).completed += OnLoadNewScene;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }

    private void OnLoadNewScene(AsyncOperation obj)
    {
        SceneManager.LoadSceneAsync(SceneToLoad).completed -= OnLoadNewScene;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void ShowAboutProgramMenu()
    {
        MainMenu.SetActive(false);
        AboutProgramMenu.SetActive(true);
    }

    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
        AboutProgramMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}