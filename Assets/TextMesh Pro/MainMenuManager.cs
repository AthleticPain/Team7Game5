using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Replace with your actual game scene name
    }

    public void OpenOptions()
    {
        // You can load an options scene or enable an options panel
        Debug.Log("Options menu opened.");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();

        // If running in editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

