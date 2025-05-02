using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    //References
    [SerializeField] public UnitVisuals unitVisuals;

    //Stats
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;

    [SerializeField] public BattleUnitStatsSO unitStats;
    [SerializeField] protected List<MoveSO> moves;
    
    //Special Status Flags
    public bool isProtected;

    public UnitStateEnum unitState;

    public bool IsDead
    {
        get { return unitState == UnitStateEnum.dead; }
    }

    // private void Start()
    // {
    //     InitializeUnit();
    // }

    public void InitializeUnit()
    {
        unitState = UnitStateEnum.alive;
        maxHP = unitStats.currentMaxHp;
        currentHP = unitStats.currentHp;
        unitVisuals.UpdateHealthBar(currentHP, maxHP);
    }

    public void TakeDamage(int power)
    {
        if (isProtected)
        {
            power /= 2;
            isProtected = false;
        }
        
        currentHP = Mathf.Min(Mathf.Max(0, currentHP - power), maxHP);
        unitVisuals?.UpdateHealthBar(currentHP, maxHP);
        Debug.Log(name + " takes " + power + " damage!\nRemaining HP: " + currentHP + "/" + maxHP);

        if (currentHP <= 0)
        {
            unitState = UnitStateEnum.dead;
            Debug.Log(name + " has died");
        }
    }

    public virtual void PlayHit()
    {
        Debug.Log($"Playing hit animation for {name}");
        if (!IsDead)
            unitVisuals?.PlayHit();
    }

    public virtual void PlayDeath()
    {
        Debug.Log($"Playing death animation for {name}");
        unitVisuals?.PlayDeath();
    }

    public virtual void PlayAttack()
    {
        Debug.Log($"Playing attack animation for {name}");
        unitVisuals?.PlayAttack();
    }

    public virtual void PlayHeal()
    {
        Debug.Log($"Playing heal animation for {name}");
    }
}