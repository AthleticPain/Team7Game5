using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "UnitStatsSO", menuName = "new UnitStatsSO")]
public class BattleUnitStatsSO : ScriptableObject
{
    //Starting base values
    [Header("Base Unit Stats")]
    [SerializeField] int baseMaxHp;
    [SerializeField] int baseStrength;
    [SerializeField] int baseSpeed;
    [SerializeField] int baseCritChance;
    
    [Header("Current Game Stats")]
    public int currentMaxHp;
    public int currentHp;
    public int strength;
    public int speed;
    public int critChance;

    public void ResetStats()
    {
        currentMaxHp = baseMaxHp;
        currentHp = currentMaxHp;
        strength = baseStrength;
        speed = baseSpeed;
        critChance = baseCritChance;
    }

    public void IncreaseStats(int additiveIncrement)
    {
        currentMaxHp += additiveIncrement;
        currentHp = currentMaxHp;
        strength += additiveIncrement;
        speed += additiveIncrement;
        critChance += additiveIncrement;
    }
}
