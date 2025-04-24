using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ChangeSceneByName(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        PlayerStatsManager.Instance.gameState = 0;
        SceneManager.LoadScene("MainMenu");
    }
}
