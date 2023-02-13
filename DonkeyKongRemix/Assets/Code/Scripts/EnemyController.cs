using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public float speed;
    public float changeTime;
    private int direction = 1;
    private float timer;

    private int health;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        health = 10;
    }

    void Update()
    {
        DirectionChange();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void DirectionChange()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    private void Move()
    {
        Vector2 position = rb2d.position;
        position.x = position.x + Time.deltaTime * speed * direction;;
        rb2d.MovePosition(position);
        if(direction == 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (direction == 1)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Damage();
        }
    }

    public void TakeDamage(int amount)
    {
        health = health - amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
