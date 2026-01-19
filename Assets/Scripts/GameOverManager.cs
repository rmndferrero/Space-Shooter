using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Don't forget this!

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public static GameOverManager instance;

    // --- NEW UI VARIABLES ---
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalHighScoreText;
    public GameObject newRecordObject; // The "New High Score!" text object
    // ------------------------

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void TriggerGameOver()
    {
        // 1. Get the scores from ScoreManager
        int currentScore = ScoreManager.instance.score;
        int highScore = ScoreManager.instance.highScore;

        // 2. Update the UI Text
        finalScoreText.text = "Score: " + currentScore.ToString();
        finalHighScoreText.text = "Best: " + highScore.ToString();

        // 3. Check for New Record
        // Logic: Since ScoreManager updates HighScore instantly while playing, 
        // if we just set a new record, currentScore will equal highScore.
        // We also check (currentScore > 0) so we don't congratulate a 0 score.
        if (currentScore >= highScore && currentScore > 0)
        {
            newRecordObject.SetActive(true); // Turn on "NEW HIGH SCORE!"
        }
        else
        {
            newRecordObject.SetActive(false); // Keep it hidden
        }

        // 4. Show the panel and freeze time
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu_Scene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}