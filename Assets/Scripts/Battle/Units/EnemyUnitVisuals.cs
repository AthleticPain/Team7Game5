using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUnitVisuals : UnitVisuals
{
    [SerializeField] private GameObject targetSymbol;
    [SerializeField] private Shadow shadow;

    public override void SetVisualState(VisualState state)
    {
        switch (state)
        {
            case VisualState.normal:
                SetDimOverlays(false);
                SetTarget(false);
                SetShadow(false);
                SetScaleUniformly(1);
                break;
            case VisualState.dimmed:
                SetDimOverlays(true);
                SetTarget(false);
                SetShadow(false);
                SetScaleUniformly(1);
                break;
            case VisualState.targeted:
                SetDimOverlays(false);
                SetTarget(true);
                SetShadow(true);
                SetScaleUniformly(1.05f);
                break;
        }
    }

    private void SetTarget(bool targetActive) => targetSymbol.SetActive(targetActive);

    private void SetShadow(bool shadowActive) => shadow.enabled = shadowActive;
    
    private void SetScaleUniformly(float scale) => transform.localScale = new Vector3(scale, scale, scale);
}