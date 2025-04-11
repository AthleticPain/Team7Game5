using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //Stats
    protected int maxHP;
    protected int currentHP;

    protected List<MoveSO> moves;
    
    public UnitStateEnum unitState;

    public void TakeDamage(int power)
    {
        currentHP -= power;
    }
}
