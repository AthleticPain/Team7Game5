using System;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Menu Fading")]
    [SerializeField] private Slider optionsSlider;
    [SerializeField] bool waitToLerp = true;
    [SerializeField] float menuAlphaThreshold = 0.2f;
    [SerializeField] float fadeSpeed = 1.0f;
    [SerializeField] private bool canLerp = false;

    [SerializeField] private CanvasGroup from, to;
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu"; // Name of your main menu scene

    private void Update()
    {
        // Fading Menus
        if (canLerp)
        {
            from.alpha -= Time.unscaledDeltaTime * fadeSpeed;
            if (from.alpha <= menuAlphaThreshold && waitToLerp || !waitToLerp)
            {
                to.gameObject.SetActive(true);
                to.alpha += Time.unscaledDeltaTime * fadeSpeed;
            }

            if (Mathf.Approximately(from.alpha, 0))
            {
                from.gameObject.SetActive(false);
            }
            if (Mathf.Approximately(to.alpha, 1))
            {
                canLerp = false;
            }
        }
    }

    // Fading Menu Functions
    public void SetFrom(CanvasGroup From) => from = From;
    public void SetTo(CanvasGroup To)
    {
        to = To;
        to.alpha = 0;
    }

    private void Awake()
    {
        // Load saved value if exists
        if (optionsSlider != null && PlayerPrefs.HasKey("SliderValue"))
        {
            float savedValue = PlayerPrefs.GetFloat("SliderValue");
            optionsSlider.value = savedValue;
            optionsSlider.onValueChanged.AddListener(HandleSliderValueChanged);
        }

        // Ensure options panel is hidden at start
        if (OptionsPanel != null)
        {
            OptionsPanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        // Clean up event listeners
        if (optionsSlider != null)
        {
            optionsSlider.onValueChanged.RemoveListener(HandleSliderValueChanged);
        }

        // Reset timescale when destroyed
        Time.timeScale = 1f;
    }

    private void HandleSliderValueChanged(float value)
    {
        // Implement what happens when slider changes
        // Example for volume control:
        // AudioListener.volume = value;

        // Save the value for next session
        PlayerPrefs.SetFloat("SliderValue", value);
    }

    public void ShowOptionsPanel()
    {
        if (OptionsPanel != null)
        {
            // Toggle the options panel visibility
            OptionsPanel.SetActive(!OptionsPanel.activeSelf);

            // Optional: Pause game when options are shown
            Time.timeScale = OptionsPanel.activeSelf ? 0f : 1f;
        }
        else
        {
            Debug.LogWarning("Options Panel reference not set in MainMenuManager");
        }
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

    public void StartNewGame()
    {
        PlayerStatsManager.Instance.GameState = 0;
        ChangeSceneFromName("MapTest");
    }

    // New function to return to main menu
    public void GoBackToMainMenu()
    {
        // Reset timescale in case game was paused
        Time.timeScale = 1f;

        // Load the main menu scene
        ChangeSceneFromName(mainMenuSceneName);
    }

    public void ChangeSceneFromIndex(int sceneIndex) => SceneManager.LoadScene(sceneIndex);
    public void ChangeSceneFromName(string sceneName) => SceneManager.LoadScene(sceneName);
    public void Quit() => Application.Quit();
}
