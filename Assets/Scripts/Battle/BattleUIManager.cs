using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform playerActionsPanel;
    [SerializeField] private RectTransform gameOverPanel;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private GameObject dimOverlayBG;

    public void SetDimBackground(bool dimBackgroundActive) => dimOverlayBG.SetActive(dimBackgroundActive);
    
    public void ShowPlayerActionsPanel()
    {
        //DoTween player actions panel in

        //playerActionsPanel.gameObject.SetActive(true);
        playerActionsPanel.DOAnchorPos(Vector2.zero, 0.2f).SetEase(Ease.OutBack);
    }

    public void HidePlayerActionsPanel()
    {
        //DoTween player actions panel out

        //playerActionsPanel.gameObject.SetActive(false);
        RectTransform parentRect = playerActionsPanel.parent as RectTransform;

        playerActionsPanel.DOAnchorPos(new Vector2(parentRect.rect.width, 0f), 0.2f)
            .SetEase(Ease.InBack);
    }

    public void ShowGameOverPanel(string gameOverTextMessage)
    {
        gameOverText.text = gameOverTextMessage;
        gameOverPanel.gameObject.SetActive(true);
    }
}