using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private string currentScene;
    private Scene activeScene;

    void Update()
    {
        activeScene = SceneManager.GetActiveScene();
        currentScene = activeScene.name;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}