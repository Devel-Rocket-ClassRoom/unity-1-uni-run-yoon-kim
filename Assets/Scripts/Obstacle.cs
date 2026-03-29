using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameManager gameManager;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();

            if (gameManager.energyBar.IsBooster()) // 부스터일 때만 날아감
            {
                FlyAway();
            }
            else if (!gameManager.energyBar.IsInvincible()) // 깜빡임 무적 아닐 때만 데미지
            {
                gameManager.energyBar.Sub(10f);
                player.StartInvincible();
            }
        }
    }

    private void FlyAway()
    {
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = 1f;
        rb.AddForce(new Vector2(5f, 10f), ForceMode2D.Impulse); // 날아가는 힘
        rb.angularVelocity = 360f; // 초당 360도 회전
    }
}
