using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Fading")]
    [SerializeField] bool waitToLerp = true;
    [SerializeField] float menuAlphaThreshold = 0.2f;
    [SerializeField] float fadeSpeed = 1.0f;
    [SerializeField] private bool canLerp = false;
    
    [SerializeField] private CanvasGroup from, to;

    private void Start()
    {
        PlayerStatsManager.Instance.GameState = 0;
    }

    private void Update()
    {
        // Fading Menus
        if (canLerp)
        {
            from.alpha -= Time.unscaledDeltaTime * fadeSpeed;
            if(from.alpha <= menuAlphaThreshold && waitToLerp || !waitToLerp)
            {
                to.gameObject.SetActive(true);
                to.alpha += Time.unscaledDeltaTime  * fadeSpeed;
            }

            if (from.alpha == 0)
            {
                from.gameObject.SetActive(false);
            }
            if (to.alpha == 1)
            {
                canLerp = false;
            }
        };
    }

    // Fading Menu Functions
    public void SetFrom(CanvasGroup From) => from = From;
    public void SetTo(CanvasGroup To)
    {
        to = To;
        to.alpha = 0;
    } 
    public void ChangeMenu() => canLerp = true;

    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();

        // If running in editor
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void ChangeSceneFromIndex(int sceneIndex) => SceneManager.LoadScene(sceneIndex); 
    public void ChangeSceneFromName(string sceneName) => SceneManager.LoadScene(sceneName); 
    public void Quit() => Application.Quit();
}

