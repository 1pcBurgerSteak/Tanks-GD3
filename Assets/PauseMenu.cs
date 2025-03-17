using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; 
    public TextMeshProUGUI countdownText; 

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        countdownText.gameObject.SetActive(false); 
        Time.timeScale = 0f; 
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false); // Close the pause menu immediately
        StartCoroutine(ResumeCountdown());
    }

    IEnumerator ResumeCountdown()
    {
        countdownText.gameObject.SetActive(true); 

        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString(); 
            yield return new WaitForSecondsRealtime(1f); 
        }

        countdownText.gameObject.SetActive(false); // Hide countdown text
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f; // Reset time before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadHome()
    {
        Time.timeScale = 1f; // Reset time before loading home
        SceneManager.LoadScene("Menu"); 
    }
}
