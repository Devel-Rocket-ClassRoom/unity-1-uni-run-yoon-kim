using System.Collections;
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
    private Coroutine boosterCoroutine;

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

        energyBar.Sub(5f * Time.deltaTime);

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

    public void StartBooster()
    {
        if (boosterCoroutine != null)
            StopCoroutine(boosterCoroutine);

        boosterCoroutine = StartCoroutine(BoosterRoutine());
    }

    private IEnumerator BoosterRoutine()
    {
        // 부스터 시작
        energyBar.SetBooster(true); // SetInvincible도 같이 true
        ScrollingObject.speed = 20f;
        player.SetAnimationSpeed(2f);

        yield return new WaitForSeconds(4f);

        // 부스터 종료
        energyBar.SetBooster(false); // SetInvincible도 같이 false
        ScrollingObject.speed = 8f;
        player.SetAnimationSpeed(1f);
        boosterCoroutine = null;
    }
}
