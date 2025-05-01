using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatToScaleWith
{
    None,
    Strength,
    Speed
}

[CreateAssetMenu(fileName = "NewMove", menuName = "Battle/Move")]
[Serializable]
public class MoveSO : ScriptableObject
{
    public string moveName;
    [SerializeField] private int minDamage;
    [SerializeField] private int maxDamage;
    public StatToScaleWith scalingStat;

    public int GetDamageValue(float scalingFactor)
    {
        int damageValue = Mathf.RoundToInt(UnityEngine.Random.Range(minDamage, maxDamage) * scalingFactor);
        return damageValue;
    }
}