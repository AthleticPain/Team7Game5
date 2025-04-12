using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public void PlayAttack()
    {
        Debug.Log("Playing enemy attack animation!");
        //throw new System.NotImplementedException();
    }

    public MoveSO GetRandomMove()
    {
        return moves[Random.Range(0, moves.Count)];
    }
}
