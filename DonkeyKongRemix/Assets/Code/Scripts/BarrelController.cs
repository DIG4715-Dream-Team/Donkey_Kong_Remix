using UnityEngine;

public class BarrelController : MonoBehaviour
{
    private GameObject currentEnemy;

    private void OnCollisionEnter2D(Collision2D other)
    {
        EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            currentEnemy = other.gameObject;
            Damage();
        }
    }

    private void Damage()
    {
        currentEnemy.GetComponent<EnemyController>().TakeDamage(10);
        Destroy(gameObject);
    }
}
