using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class UnitVisuals : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public enum VisualState
    {
        normal,
        dimmed,
        targeted
    }

    [SerializeField] private Animator animator;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] protected GameObject[] dimOverlays;

    [HideInInspector] public UnityEvent OnPointerEntered = new UnityEvent();
    [HideInInspector] public UnityEvent OnPointerClicked = new UnityEvent();

    public void UpdateHealthBar(int current, int max)
    {
        float fill = (float)current / max;
        healthBar.fillAmount = fill;
        hpText.text = $"{current}/{max}";
    }

    public void PlayHit() => animator?.SetTrigger("Hit");
    public void PlayDeath() => animator?.SetTrigger("Death");
    public void PlayAttack() => animator?.SetTrigger("Attack");
    public void PlayHighlightTurn() => animator?.SetTrigger("HighlightTurn");
    public void PlayHeal() => animator?.SetTrigger("Heal");

    public virtual void SetVisualState(VisualState state)
    {
        switch (state)
        {
            case VisualState.normal:
                SetDimOverlays(false);
                break;
            case VisualState.dimmed:
                SetDimOverlays(true);
                break;
            case VisualState.targeted:
                SetDimOverlays(false);
                break;
        }
    }

    protected void SetDimOverlays(bool overlayActive)
    {
        foreach (var dimOverlay in dimOverlays)
        {
            dimOverlay.SetActive(overlayActive);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log($"Pointer entered {name}");
        OnPointerEntered?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPointerClicked?.Invoke();
    }
}