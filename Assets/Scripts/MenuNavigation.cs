using UnityEngine;

public class MenuNavigation : MonoBehaviour
{
    public GameObject creditsPanel; // Assign in Inspector

    public void ToggleCredits()
    {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }
}
