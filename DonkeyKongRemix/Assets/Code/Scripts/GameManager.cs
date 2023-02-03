using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI text1;
    private GameObject player;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        text1.text = $"Current Health:{player.GetComponent<PlayerController>().health}";
    }
}
