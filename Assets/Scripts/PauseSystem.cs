using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseMenuPanel;

    // Call this when the small Pause Button is clicked
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); // Show the menu
        Time.timeScale = 0f; // FREEZE time (physics, movement, animations stop)
    }

    // Call this when the "Resume" button is clicked
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Hide the menu
        Time.timeScale = 1f; // UNFREEZE time
    }

    // Call this when the "Quit" button is clicked
    public void QuitToMenu()
    {
        // IMPORTANT: Always unfreeze time before leaving the scene!
        // If you don't, the next time you play, the game will still be frozen.
        Time.timeScale = 1f;

        SceneManager.LoadScene("Menu_Scene"); // Replace with your actual menu scene name
    }
}