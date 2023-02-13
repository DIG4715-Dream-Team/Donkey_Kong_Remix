using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI health;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private TextMeshProUGUI score;

    private string currentScene;
    private Scene activeScene;

    private bool gamePaused = false;
    [SerializeField]
    private GameObject pauseMenu;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        activeScene = SceneManager.GetActiveScene();
        currentScene = activeScene.name;
        updateText();

        if (currentScene != "DKCR_MainMenu" &&  Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            gamePaused = true;
        }

        if (gamePaused == true)
        {
            pauseMenu.SetActive(true);
        }

        if (Time.timeScale == 1)
        {
            pauseMenu.SetActive(false);
        }
    }

    private void updateText()
    {
        health.text = $"Current Health:{player.GetComponent<PlayerController>().Health}";
        score.text = $"Score: {player.GetComponent<PlayerController>().Score}";
    }
}
