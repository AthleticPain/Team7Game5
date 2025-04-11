using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveExecutionState : BattleStateBase
{
    private MoveSO move;

    public PlayerMoveExecutionState(BattleSystem battle, MoveSO move) : base(battle)
    {
        this.move = move;
    }

    public override void Enter()
    {
        battle.StartCoroutine(ExecuteMove());
    }

    private IEnumerator ExecuteMove()
    {
        // var player = battle.GetCurrentPlayerUnit();
        // var target = battle.GetSelectedEnemy();
        //
        // player.PlayAttack();
        // yield return new WaitForSeconds(0.5f);
        //
        // target.TakeDamage(move.Power);
        // target.PlayHit();
        //
        // yield return new WaitForSeconds(0.5f);
        //
        // if (target.IsDead())
        // {
        //     target.PlayDeath();
        //     yield return new WaitForSeconds(0.5f);
        // }
        //
        // battle.OnEnemyTurn();

        yield return new NotImplementedException();
    }
}

