using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI text1;

    void Start()
    {
        text1.text = "Heres some text";
    }

    void Update()
    {
        
    }
}
