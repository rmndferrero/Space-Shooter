using UnityEngine;
using UnityEngine.SceneManagement; // Needed to change scenes

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Replace "GameLevel" with the EXACT name of your scene file
        SceneManager.LoadScene("Main_Scene");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game Request Received"); // Shows in editor because Quit() doesn't work in editor
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif  
    }
}