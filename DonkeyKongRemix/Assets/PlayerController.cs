using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float secondJumpForce;

    private bool isOnGround = true;
    private bool isMidAir = false;
    private bool canDoubleJump = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
        Jump();
    }

    private void Movement()
    {
        if (rb2d.bodyType == RigidbodyType2D.Dynamic)
        {
            float hozMovement = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(hozMovement * speed, rb2d.velocity.y);
        }
    }

    //Add mid jump when just falling "not inside of move as if(rb2b.velocity.y != 0) this will cause else if(isMidAir... to run forever. 
    private void Jump()
    {
        if (isOnGround == true && Input.GetKey(KeyCode.W))
        {
            isOnGround = false;
            isMidAir = true;
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            canDoubleJump = true;
            Debug.Log("groundjump");
        }
        else if (isMidAir == true && Input.GetKey(KeyCode.W))
        {
            isMidAir = false;
            canDoubleJump = false;
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Debug.Log("midair");
        }
        else if (canDoubleJump == true && Input.GetKey(KeyCode.W))
        {
            canDoubleJump = false;
            isMidAir = false;
            rb2d.AddForce(new Vector2(0, secondJumpForce), ForceMode2D.Impulse);
            Debug.Log("doublejump");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && isOnGround == false)
        {
            isOnGround = true;
            canDoubleJump = true;
            isMidAir = false;
        }
    }
}
