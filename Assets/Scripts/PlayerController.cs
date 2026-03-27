using NUnit.Framework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead;
    private float jumpForce = 5f;
    private int jumpCount = 0;
    private bool isGrounded = false;

    private Rigidbody2D playerRigidBody;
    private Animator animator;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        if (Input.GetButtonDown("Fire1") && jumpCount < 2)
        {
            jumpCount++;
            playerRigidBody.linearVelocity = Vector2.zero;
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else if (Input.GetButtonUp("Fire1") && playerRigidBody.linearVelocity.y > 0)
        {
            playerRigidBody.linearVelocity *= 0.5f;
        }

        animator.SetBool("Grounded", isGrounded);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dead") && !isDead)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform")) // && collision.contacts[0].normal.y > 0.7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            isGrounded = false;
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
    }
}
