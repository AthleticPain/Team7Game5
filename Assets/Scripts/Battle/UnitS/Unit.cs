using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    //Stats
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;

    [SerializeField] protected List<MoveSO> moves;

    public UnitStateEnum unitState;

    public bool IsDead
    {
        get { return unitState == UnitStateEnum.dead; }
    }

    public void TakeDamage(int power)
    {
        currentHP -= power;
        Debug.Log(name + " takes " + power + " damage!\nRemaining HP: " + currentHP + "/" + maxHP);

        if (currentHP <= 0)
        {
            unitState = UnitStateEnum.dead;
            Debug.Log(name + " has died");
        }
    }

    public abstract void PlayHit();
    public abstract void PlayDeath();
    public abstract void PlayAttack();
}