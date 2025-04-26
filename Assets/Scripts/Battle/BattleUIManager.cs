using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] private RectTransform playerActionsPanel;
    [SerializeField] private RectTransform gameOverPanel;
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

    public void ShowGameOverPanel(bool win)
    {
        gameOverPanel.gameObject.SetActive(true);
        
        //Child 0 is win panel, child 1 is lose panel
        gameOverPanel.transform.GetChild(0).gameObject.SetActive(win);
        gameOverPanel.transform.GetChild(1).gameObject.SetActive(!win);
    }
}