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

    private bool canJump= true;
    private bool isJumping = false;
    private bool canDoubleJump = false;
    private bool isDoubleJumping = false;

    public int Health { get; private set; }

    private GameObject currentEnemy;
    private GameObject currentBarrel;
    [SerializeField]
    private GameObject player;
    private bool EnemyInRange = false;
    private bool isAttacking = false;
    private bool BarrelInRange = false;
    private bool BarrelIsGrabbed = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        Health = 50;
    }

    private void Update()
    {
        Jump();
        Attack();
        BarrelControls();
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
        if (canJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            canJump = false;
            isJumping = true;
            canDoubleJump = true;
            rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        else if (isJumping == true && Input.GetKeyUp(KeyCode.Space))
        {
            canJump = false;
        }

        else if (canDoubleJump == true && Input.GetKeyDown(KeyCode.Space))
        {
            isDoubleJumping = true;
            canDoubleJump = false;
            rb2d.AddForce(new Vector2(0, secondJumpForce), ForceMode2D.Impulse);
        }
        else if (isDoubleJumping == true && Input.GetKeyUp(KeyCode.Space))
        {
            canJump = false;
            canDoubleJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentEnemy = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentEnemy = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null && other.gameObject.CompareTag("Enemy"))
        {
            EnemyInRange = true;
            currentEnemy = other.gameObject;
        }
        else if (other != null && other.gameObject.CompareTag("Barrel"))
        {
            BarrelInRange = true;
            currentBarrel = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null && other.gameObject.CompareTag("Enemy"))
        {
            EnemyInRange = false;
            currentEnemy = null;
        }
        else if (other != null && other.gameObject.CompareTag("Barrel"))
        {
            BarrelInRange = false;
            currentBarrel = null;
        }
    }

    private void BarrelControls()
    {
        if (BarrelInRange == true && Input.GetKeyDown(KeyCode.F))
        {
            currentBarrel.transform.parent = player.transform;
            currentBarrel.transform.position = rb2d.transform.position;
            currentBarrel.transform.rotation = Quaternion.Euler(0,0,90);
            currentBarrel.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            rb2d.mass = rb2d.mass + currentBarrel.GetComponent<Rigidbody2D>().mass;
            BarrelIsGrabbed = true;
            BarrelInRange = false;
        }

        if (BarrelIsGrabbed == true)
        {
            currentBarrel.transform.position = rb2d.transform.position;
            currentBarrel.transform.Translate(2,0,0);
        }

        if (BarrelIsGrabbed == true && Input.GetKeyDown(KeyCode.R))
        {
            currentBarrel.transform.parent = null;
            currentBarrel.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            currentBarrel.GetComponent<Rigidbody2D>().AddForce(new Vector2(10,5),ForceMode2D.Impulse);
            rb2d.mass = rb2d.mass - currentBarrel.GetComponent<Rigidbody2D>().mass;
            BarrelIsGrabbed = false;
        }

        if (BarrelIsGrabbed == true && Input.GetKeyDown(KeyCode.E))
        {
            currentBarrel.transform.parent = null;
            currentBarrel.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            currentBarrel.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 5), ForceMode2D.Impulse);
            rb2d.mass = rb2d.mass - currentBarrel.GetComponent<Rigidbody2D>().mass;
            BarrelIsGrabbed = false;
        }
    }

    private void Attack()
    {
        if (EnemyInRange == true && isAttacking == false && Input.GetKeyDown(KeyCode.Q))
        {
            currentEnemy.GetComponent<EnemyController>().TakeDamage(5);
            isAttacking = true;
        }
        else if (isAttacking == true && Input.GetKeyUp(KeyCode.Q))
        {
            isAttacking = false;
        }
    }

    public void Damage()
    {
        Health = Health - 5;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && canJump == false)
        {
            canJump = true;
        }
    }
}
