using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isDead;
    private float jumpForce = 60f;
    private float sldieForce = 120f;
    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool isSlide = false;

    private Rigidbody2D playerRigidBody;
    private Animator animator;
    private CircleCollider2D runCollider;
    private BoxCollider2D slideCollider;

    private List<Collision2D> platformList;

    public GameManager gameManager;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        runCollider = GetComponent<CircleCollider2D>();
        slideCollider = GetComponent<BoxCollider2D>();
        platformList = new List<Collision2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slideCollider.enabled = false;
        runCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
            return;

        if (Input.GetButtonDown("Fire1") && jumpCount < 2)
        {
            jumpCount++;
            isSlide = false;
            animator.SetBool("IsSlide", false);
            playerRigidBody.linearVelocity = Vector2.zero;
            playerRigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            isSlide = true;
            playerRigidBody.linearVelocity = Vector2.zero;
            playerRigidBody.AddForce(Vector2.down * sldieForce, ForceMode2D.Impulse);
            animator.SetBool("IsSlide", isSlide && isGrounded);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isSlide = false;
            animator.SetBool("IsSlide", isSlide);
        }

        if (isSlide)
        {
            slideCollider.enabled = true;
            runCollider.enabled = false;
        }
        else
        {
            slideCollider.enabled = false;
            runCollider.enabled = true;
        }

        if (platformList.Count == 0)
        {
            isGrounded = false;
        }
        else
        {
            isGrounded = true;
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
            //isGrounded = true;
            platformList.Add(collision);
            jumpCount = 0;

            animator.SetBool("IsSlide", isSlide);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform") && platformList.Count > 0)
        {
            //isGrounded = false;
            platformList.RemoveAt(0);
        }
    }

    private void Die()
    {
        playerRigidBody.linearVelocity = Vector2.zero;
        playerRigidBody.bodyType = RigidbodyType2D.Kinematic;
        isDead = true;
        animator.SetTrigger("Die");
        ScrollingObject.speed = 0f;
        gameManager.OnPlayerDead();
    }
}
