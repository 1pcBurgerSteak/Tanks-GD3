using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public int score = 0;
    public int wave = 0;
    public int life = 3;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI timerText;

    public GameObject gameoverPanel;
    public TextMeshProUGUI scorePanel;
    public TextMeshProUGUI hiScorePanel;
    public GameObject camera1;

    public GameObject pauseButton;
    private float countdownTime = 0f; // Tracks the countdown timer
    private bool isCountingDown = false;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    void Start()
    {
        
        UpdateWave();
        StartCountdown(3);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCountingDown)
        {
            
            CountdownLogic();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            UpdateScore(10);
            UpdateWave();
            UpdateLife();
        }
    }

    public void NewGame()
    {
        score = 0;
        wave = 0;
        life = 0;
    }

    public void GameOver()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score >= highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        
        gameoverPanel.SetActive(true);
        scorePanel.text = $"{score}";
        hiScorePanel.text = $"{PlayerPrefs.GetInt("HighScore", 0)}";
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
    }

    public void UpdateScore(int add)
    {
        score += add;
        scoreText.text = $"{score.ToString("D5")}";
    }

    public void UpdateWave()
    {
        wave += 1;
        waveText.text = $"Wave: {wave.ToString("D2")}";
    }

    public void UpdateLife()
    {
        camera1.transform.position = new Vector3(0, 0, 0);
        life -= 1;
        if (life <= 0)
        {
            GameOver();
            Time.timeScale = 0;
        }
        lifeText.text = $"x: {life}";
    }

    public void StartCountdown(int seconds)
    {
        pauseButton.SetActive(false);
        countdownTime = seconds;
        isCountingDown = true;
        Time.timeScale = 0;
        timerText.gameObject.SetActive(true);

        DestroyShells();
    }

    private void CountdownLogic()
    {
        countdownTime -= Time.unscaledDeltaTime;

        if (countdownTime > 0)
        {
            timerText.text = Mathf.Ceil(countdownTime).ToString();
        }
        else
        {
            timerText.text = "";
            timerText.gameObject.SetActive(false);
            Time.timeScale = 1;
            isCountingDown = false;
            pauseButton.SetActive(true);
        }
    }

    void DestroyShells()
    {
        GameObject[] shells = GameObject.FindGameObjectsWithTag("Shells");

        foreach (GameObject shell in shells)
        {
            Destroy(shell);
        }

        Debug.Log($"Destroyed {shells.Length} shells.");
    }


}
