using UnityEngine;
using UnityEngine.SceneManagement; // Needed to change scenes

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    // Singleton pattern so the Player can find this easily
    public static GameOverManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void TriggerGameOver()
    {
        // 1. Show the cursor so the player can click
        // Cursor.visible = true; (Only needed if you hide it during gameplay)

        // 2. Turn on the Game Over screen
        gameOverPanel.SetActive(true);

        // 3. Freeze time so enemies stop moving
        Time.timeScale = 0f;
    }

    public void RestartLevel()
    {
        // Unfreeze time before reloading!
        Time.timeScale = 1f;
        // Reloads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu_Scene"); // Make sure this matches your Menu scene name exactly!
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