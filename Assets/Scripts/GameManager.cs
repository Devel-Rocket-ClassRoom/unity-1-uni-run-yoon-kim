using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOverUI;

    private int score = 0;
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            ScrollingObject.speed = 10f;
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
