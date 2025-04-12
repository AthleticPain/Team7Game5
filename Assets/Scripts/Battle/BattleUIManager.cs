using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform playerActionsPanel;
    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;

    public void ShowPlayerActionsPanel()
    {
        //DoTween player actions panel in
        playerActionsPanel.gameObject.SetActive(true);
    }

    public void HidePlayerActionsPanel()
    {
        //DoTween player actions panel out
        playerActionsPanel.gameObject.SetActive(false);
    }

    public void ShowGameOverPanel(string gameOverTextMessage)
    {
        gameOverText.text = gameOverTextMessage;
        gameOverPanel.gameObject.SetActive(true);
    }
    
    
}
