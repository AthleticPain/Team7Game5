using UnityEngine;
using UnityEngine.UI;  // For UI elements
using UnityEngine.SceneManagement;  // For loading scenes (optional for Quit functionality)

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Reference to the pause menu UI
    public GameObject player;  // Reference to the player or game objects you may want to disable while paused

    private bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        // Toggle pause menu visibility when the player presses the "Escape" key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Function to pause the game
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // Activate the pause menu UI
        Time.timeScale = 0f;  // Stop the game time (pauses all physics and animations)
        isPaused = true;
    }

    // Function to resume the game
    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);  // Deactivate the pause menu UI
        Time.timeScale = 1f;  // Resume normal game time
        isPaused = false;
    }

    // Function to quit the game (optional)
    public void QuitGame()
    {
        // Optionally, save the game state or settings here
        Debug.Log("Quit Game!");
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;  // Stops play mode in the Unity Editor
#endif
    }
}

