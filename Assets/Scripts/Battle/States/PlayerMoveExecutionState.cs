using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveExecutionState : BattleStateBase
{
    private MoveSO move;
    private EnemyUnit targetEnemy;

    public PlayerMoveExecutionState(BattleSystem battle, MoveSO move, EnemyUnit targetEnemy) : base(battle)
    {
        this.move = move;
        this.targetEnemy = targetEnemy;
    }

    public override void Enter()
    {
        battle.StartCoroutine(ExecuteMove());
    }

    private IEnumerator ExecuteMove()
    {
        var player = battle.GetCurrentUnitAs<PlayerUnit>();
        
        if(player == null)
        {
            Debug.LogError("Something went wrong");
        }
        
        player.PlayAttack();
        yield return new WaitForSeconds(1f);
        
        targetEnemy.TakeDamage(move.power);
        targetEnemy.PlayHit();
        
        yield return new WaitForSeconds(1.5f);
        
        if (targetEnemy.IsDead)
        {
            targetEnemy.PlayDeath();
            yield return new WaitForSeconds(0.5f);
        }
        
        battle.OnTurnEnded();
    }
}

