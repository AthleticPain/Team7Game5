using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public MoveSO GetRandomMove()
    {
        return moves[Random.Range(0, moves.Count)];
    }
    
    // public override void PlayAttack()
    // {
    //     Debug.Log("Playing enemy attack animation!");
    //     //throw new System.NotImplementedException();
    // }
    //
    // public override void PlayHit()
    // {
    //     Debug.Log("Playing enemy hit animation!");
    // }
    //
    // public override void PlayDeath()
    // {
    //     Debug.Log("Playing enemy death animation!");
    // }
}
