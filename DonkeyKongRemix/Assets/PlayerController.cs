using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (rb2d.bodyType == RigidbodyType2D.Dynamic)
        {
            float hozMovement = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(hozMovement * speed, rb2d.velocity.y);
        }
    }

    private void Jump()
    {
        //if (Input.GetKey(KeyCode.W) && isOnGround == true)
        if (Input.GetKey(KeyCode.W))
        {
            //isOnGround = false;
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
