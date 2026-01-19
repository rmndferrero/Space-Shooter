using UnityEngine;
using TMPro; // Needed for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; // Singleton so others can access it

    public TextMeshProUGUI scoreText;      // Your current score
    public TextMeshProUGUI highScoreText;  // The permanent high score

    public int score = 0;
    public int highScore = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        score = 0;

        // 1. Load the High Score from the computer's memory
        // "HighScore" is the key name. 0 is the default if no save exists.
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateScoreUI();
    }

    public void AddScore(int points)
    {
        score += points;

        // 2. Check if we beat the High Score
        if (score > highScore)
        {
            highScore = score;

            // 3. Save the new High Score to memory immediately
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        // Update both texts
        scoreText.text = "Score: " + score.ToString();

        if (highScoreText != null)
        {
            highScoreText.text = "Best: " + highScore.ToString();
        }
    }
}