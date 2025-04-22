using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditsPanel;

    // Called when the "Credits" button is clicked in the main menu
    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);   // Hides the main menu
        creditsPanel.SetActive(true);     // Shows the credits screen
    }

    // Called when the "Back" button is clicked on the credits screen
    public void BackToMenu()
    {
        creditsPanel.SetActive(false);    // Hides the credits screen
        mainMenuPanel.SetActive(true);    // Shows the main menu
    }
}

