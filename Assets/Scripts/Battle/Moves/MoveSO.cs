using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatToScaleWith
{
    None,
    Strength,
    Speed,
    Health
}

[CreateAssetMenu(fileName = "NewMove", menuName = "Battle/Move")]
[Serializable]
public class MoveSO : ScriptableObject
{
    public string moveName;
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    public StatToScaleWith scalingStat;

    public int GetDamageValue(BattleUnitStatsSO unitStats)
    {
        float scalingFactor = 1;
        switch (scalingStat)
        {
            case StatToScaleWith.Strength:
                scalingFactor = unitStats.strength;
                break;
            case StatToScaleWith.Speed:
                scalingFactor = unitStats.speed;
                break;
            case StatToScaleWith.Health:
                scalingFactor = unitStats.currentMaxHp * 0.01f;
                break;
            default:
                break;
        }
        int damageValue = Mathf.FloorToInt(UnityEngine.Random.Range(minDamage, maxDamage) * scalingFactor);
        return damageValue;
    }
}