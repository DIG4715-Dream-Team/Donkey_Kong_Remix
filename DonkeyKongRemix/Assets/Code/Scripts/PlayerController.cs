using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
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

    public int health { get; private set; }

    [SerializeField]
    public TextMeshProUGUI text1;

    private GameObject currentEnemy;
    private bool InRange = false;
    private bool isAttacking = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        health = 50;
    }

    private void Update()
    {
        Jump();
        Attack();
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
        if (other.gameObject.tag == "Enemy")
        {
            currentEnemy = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            currentEnemy = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            InRange = true;
            currentEnemy = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other != null)
        {
            InRange = false;
            currentEnemy = null;
        }
    }

    private void Attack()
    {
        if (InRange == true && isAttacking == false && Input.GetKeyDown(KeyCode.F))
        {
            currentEnemy.GetComponent<EnemyController>().TakeDamage(5);
            isAttacking = true;
        }
        else if (isAttacking == true && Input.GetKeyUp(KeyCode.F))
        {
            isAttacking = false;
        }
    }

    public void Damage()
    {
        health = health - 5;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground") && canJump == false)
        {
            canJump = true;
        }
    }
}
