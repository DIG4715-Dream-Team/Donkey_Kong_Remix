using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI text1;
    private GameObject player;

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
        UpdateHealth();

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

    private void UpdateHealth()
    {
        text1.text = $"Current Health:{player.GetComponent<PlayerController>().Health}";
    }
}
