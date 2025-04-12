using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    //Stats
    [SerializeField] protected int maxHP;
    [SerializeField] protected int currentHP;

    [SerializeField] protected List<MoveSO> moves;
    
    public UnitStateEnum unitState;

    public void TakeDamage(int power)
    {
        Debug.Log("Player takes " + power + " damage!");
        currentHP -= power;
    }
}
