using System.Collections;
using TMPro;
using UnityEngine;

public class SingleplayerManager : MonoBehaviour
{
    public GameObject player;
    public GameObject cameraRig;

    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI waveText;

    public TextMeshProUGUI spaceText;
    public TextMeshProUGUI timerText; // Assign a TextMeshProUGUI component in the Inspector

    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI highScoreText;

    private float timer = 3f;
    private bool isFirstSpawn = false; // Tracks the first-time spawn
    private bool timerActive = false;

    public int lives = 3; // Initial number of lives
    public int score = 0; // Initial score
    public int wave = 1; // Start with Wave 1

    void Start()
    {
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false); // Initially hide the timer
        }

        // Show spaceText at the start of the game
        spaceText.gameObject.SetActive(true);
        Time.timeScale = 0; // Pause the game at the start

        // Initialize life, score, and wave text
        UpdateLifeText();
        UpdateScoreText();
        UpdateWaveText();
    }

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            Time.timeScale = 0; // Pause the game if the player object is not spawned
            return;
        }

        if (!isFirstSpawn && player.activeInHierarchy)
        {
            spaceText.gameObject.SetActive(false); // Hide the space text once the player is spawned
            StartTimer(); // Start countdown for first spawn
            isFirstSpawn = true;
        }

        if (timerActive)
        {
            timer -= Time.unscaledDeltaTime; // Countdown using unscaledDeltaTime

            if (timer > 0)
            {
                timerText.text = Mathf.Ceil(timer).ToString(); // Display the countdown as whole numbers
            }
            else
            {
                timerText.gameObject.SetActive(false); // Hide the timer once finished
                timerActive = false;
                player.SetActive(true); // Activate the player
                Time.timeScale = 1; // Resume game time
            }
        }

        // Pause if the player is inactive
        if (!player.activeInHierarchy && lives > 0)
        {
            Time.timeScale = 0; // Pause the game
            StartTimer(); // Start countdown for respawn
            ResetPlayer(); // Reset the player's position
        }
    }

    // Function to start the countdown timer
    private void StartTimer()
    {
        timer = 3f;
        timerActive = true;
        timerText.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    // Function to reset the player and trigger the countdown again
    public void ResetPlayer()
    {
        if (player != null)
        {
            player.transform.position = new Vector3(0, 1, 0); // Reset the player position
            cameraRig.transform.position = new Vector3(0, 0, 0); // Reset the camera position
            player.SetActive(true); // Activate the player
            StartTimer(); // Trigger the 3, 2, 1 countdown
        }
    }

    // Function to decrease lives and update the text
    public void Life()
    {
        lives--; // Reduce lives by 1
        lifeText.text = $"Lives: {lives}"; // Update the life text

        if (lives <= 0)
        {
            
            GameOver();
        }
    }

    private void GameOver()
    {
        // Save and display final score
        finalScoreText.text = $"Final Score: {score}";

        // Retrieve the saved high score
        int savedHighScore = PlayerPrefs.GetInt("HighScore", 0);

        // Update high score if current score is greater
        if (score > savedHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = $"High Score: {score}";
        }
        else
        {
            highScoreText.text = $"High Score: {savedHighScore}";
        }

        // Display the game over panel
        gameOverPanel.SetActive(true);

        // Pause the game
        Time.timeScale = 0;
    }

    // Function to add score and update the text
    public void AddScore(int points)
    {
        score += points; // Add the given points to the score
        scoreText.text = $"Score: {score}"; // Format the score and update the text
        Debug.Log($"Score added: {points}. Total Score: {score}"); // Format the score and update the text
    }

    // Function to increment wave and update the text
    public void Wave(int wave)
    { // Increment the wave count
        waveText.text = $"Wave: {wave:00}"; // Format the wave and update the text
    }

    // Helper method to update the life text
    private void UpdateLifeText()
    {
        lifeText.text = $"Lives: {lives}";
    }

    // Helper method to update the score text
    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score:0000}"; // Format the score with leading zeroes
    }

    // Helper method to update the wave text
    private void UpdateWaveText()
    {
        waveText.text = $"Wave: {wave:00}"; // Format the wave with leading zeroes
    }
}
