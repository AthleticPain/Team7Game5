using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitVisuals : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI hpText;

    public void UpdateHealthBar(int current, int max)
    {
        float fill = (float)current / max;
        healthBar.fillAmount = fill;
        hpText.text = $"{current}/{max}";
    }

    public void PlayHit() => animator?.SetTrigger("Hit");
    public void PlayDeath() => animator?.SetTrigger("Die");
    public void PlayAttack() => animator?.SetTrigger("Attack");
    public void PlayHighlightTurn() => animator?.SetTrigger("HighlightTurn");
}