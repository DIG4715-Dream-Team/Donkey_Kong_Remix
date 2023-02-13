using UnityEngine;

public class BananaController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.ScoreChange(1);
            Destroy(gameObject);
        }
    }
}
