using System.Collections;
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
    private SpriteRenderer spriteRenderer;

    private List<Collision2D> platformList;

    public GameManager gameManager;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        runCollider = GetComponent<CircleCollider2D>();
        slideCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            if (isGrounded)
            {
                slideCollider.enabled = true;
                runCollider.enabled = false;
            }
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
            gameManager.energyBar.Sub(100f);
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

    public void StartInvincible()
    {
        if (!gameManager.energyBar.IsInvincible())
            StartCoroutine(InvincibleRoutine());
    }

    private IEnumerator InvincibleRoutine()
    {
        gameManager.energyBar.SetInvincible(true); // EnergyBar에 위임

        float duration = 3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        spriteRenderer.enabled = true;
        gameManager.energyBar.SetInvincible(false);
    }

    public void Die()
    {
        playerRigidBody.linearVelocity = Vector2.zero;
        playerRigidBody.bodyType = RigidbodyType2D.Kinematic;
        isDead = true;
        animator.SetTrigger("Die");
        ScrollingObject.speed = 0f;
        gameManager.OnPlayerDead();
    }

    public void SetAnimationSpeed(float speed)
    {
        animator.speed = speed;
    }
}
