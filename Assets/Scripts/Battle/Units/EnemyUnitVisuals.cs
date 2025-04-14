using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnitVisuals : UnitVisuals
{
    [SerializeField] private Image enemyImage;
    [SerializeField] private Outline highlightOutline;

    public Unit Unit { get; private set; }

    public void Initialize(Unit unit)
    {
        Unit = unit;
    }

    public void SetHighlighted(bool highlighted)
    {
        highlightOutline.enabled = highlighted;
        enemyImage.color = highlighted ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f); // dim if not highlighted
    }
}
