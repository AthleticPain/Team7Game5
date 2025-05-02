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

[CreateAssetMenu(fileName = "NewMove", menuName = "Moves/Attack")]
public class Attack : MoveSO
{
    [SerializeField] private float minDamage;
    [SerializeField] private float maxDamage;
    public StatToScaleWith scalingStat;

    private int GetDamageValue(BattleUnitStatsSO unitStats)
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
    
    public override IEnumerator ExecuteMove(Unit moveUser, Unit[] targetUnits)
    {
        Debug.Log($"{moveUser.name} casting {moveName} attack on:");

        foreach (Unit targetUnit in targetUnits)
        {
            Debug.Log(targetUnit.name);
        }
        
        int damageValue = GetDamageValue(moveUser.unitStats);

        if (moveUser == null)
        {
            Debug.LogError("Something went wrong");
        }

        if (damageValue >= 0)
        {
            moveUser.PlayAttack();
            yield return new WaitForSeconds(1f);
        }

        foreach (Unit targetUnit in targetUnits)
        {
            Debug.Log($"{moveUser.name} is attacking {targetUnit.name} with {name}.");


            targetUnit.TakeDamage(damageValue);
            if (damageValue > 0)
                targetUnit.PlayHit();
            else
                targetUnit.PlayHeal();

            if (targetUnit.IsDead)
            {
                targetUnit.PlayDeath();
                //yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
