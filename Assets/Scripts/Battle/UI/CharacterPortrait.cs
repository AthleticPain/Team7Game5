using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CharacterPortraitState
{
    neutral,
    hurt,
    attack
}

public class CharacterPortrait : MonoBehaviour
{
    [SerializeField] private Image portraitImage;
    [SerializeField] private float hpRatio;
    [SerializeField] private CharacterPortraitState currentState;
    
    //0->Neutral
    //1->Hurt
    //2->Attack
    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] damagedSprites;

    public void UpdatePortraitState(CharacterPortraitState state)
    {
        currentState = state;
        if (hpRatio < 0.5f)
        {
            portraitImage.sprite = damagedSprites[(int)currentState];
        }
        else
        {
            portraitImage.sprite = normalSprites[(int)currentState];
        }
    }

    public void UpdateHPRatio(float hpRatio)
    {
        this.hpRatio = hpRatio;
        if (this.hpRatio < 0.5f)
        {
            portraitImage.sprite = damagedSprites[(int)currentState];
        }
        else
        {
            portraitImage.sprite = normalSprites[(int)currentState];
        }
    }
}
