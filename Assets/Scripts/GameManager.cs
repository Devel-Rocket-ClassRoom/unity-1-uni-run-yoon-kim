using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;
    public EnergyBar energyBar;
    public PlayerController player;

    private int score = 0;
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                ScrollingObject.speed = 10f;
            }
            return;
        }

        energyBar.Sub(3f * Time.deltaTime);

        if (energyBar.IsEmpty())
        {
            player.Die();
        }
    }

    public void AddScore(int add)
    {
        score += add;
        scoreText.text = $"SCORE: {score}";
    }

    public void OnPlayerDead()
    {
        IsGameOver = true;
        gameOverUI.SetActive(true);
    }
}
