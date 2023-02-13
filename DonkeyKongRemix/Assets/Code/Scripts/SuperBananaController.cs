using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuperBananaController : MonoBehaviour
{
    private string currentScene;
    private Scene activeScene;

    [SerializeField]
    private TextMeshProUGUI win;

    [SerializeField]
    private GameObject Restart;

    void Update()
    {
        activeScene = SceneManager.GetActiveScene();
        currentScene = activeScene.name;

        if (currentScene != "DKCR_MainMenu" && Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            Restart.SetActive(true);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null && currentScene == "Jungle Level")
        {
            SceneManager.LoadScene("Lab Level");
        }
        else if (player != null && currentScene == "Lab Level") ;
        {
            win.text = "You have won Gorilla Country";
            Time.timeScale = 0;
        }
    }
}
