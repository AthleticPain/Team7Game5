using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    //References
    [SerializeField] public UnitVisuals unitVisuals;
    [SerializeField] public CharacterPortrait portrait;

    //Stats
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;

    [SerializeField] public BattleUnitStatsSO unitStats;
    [SerializeField] protected List<MoveSO> moves;

    //Special Status Flags
    [SerializeField] private bool isProtected;
    [SerializeField] private bool isVulnerable;

    public UnitStateEnum unitState;

    public bool IsDead
    {
        get { return unitState == UnitStateEnum.dead; }
    }

    public bool IsProtected
    {
        get { return isProtected; }
        set
        {
            unitVisuals?.ShowBuffIcon(value);
            isProtected = value;
        }
    }

    public bool IsVulnerable
    {
        get { return isVulnerable; }
        set
        {
            unitVisuals?.ShowDebuffIcon(value);
            isVulnerable = value;
        }
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
        if (IsProtected)
        {
            power /= 2;
            IsProtected = false;
        }

        if (IsVulnerable)
        {
            power *= 2;
            IsVulnerable = false;
        }

        currentHP = Mathf.Min(Mathf.Max(0, currentHP - power), maxHP);
        unitVisuals?.UpdateHealthBar(currentHP, maxHP);
        portrait?.UpdateHPRatio((float)currentHP/maxHP);
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
        {
            unitVisuals?.PlayHit();
            portrait?.UpdatePortraitState(CharacterPortraitState.hurt);
        }
    }

    public virtual void PlayDeath()
    {
        Debug.Log($"Playing death animation for {name}");
        unitVisuals?.PlayDeath();
        portrait?.UpdatePortraitState(CharacterPortraitState.hurt);
    }

    public virtual void PlayAttack()
    {
        Debug.Log($"Playing attack animation for {name}");
        unitVisuals?.PlayAttack();
        portrait?.UpdatePortraitState(CharacterPortraitState.attack);
    }

    public virtual void PlayHeal()
    {
        Debug.Log($"Playing heal animation for {name}");
        portrait?.UpdatePortraitState(CharacterPortraitState.neutral);
    }

    public virtual void PlayDefault()
    {
        unitVisuals?.PlayDefault();
        portrait?.UpdatePortraitState(CharacterPortraitState.neutral);
    }
}