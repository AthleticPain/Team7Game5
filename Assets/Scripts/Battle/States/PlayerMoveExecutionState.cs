using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveExecutionState : BattleStateBase
{
    private MoveSO move;
    private Unit[] targetUnits;

    public PlayerMoveExecutionState(BattleSystem battle, MoveSO move, Unit[] targetUnits) : base(battle)
    {
        this.move = move;
        this.targetUnits = targetUnits;
    }

    public override void Enter()
    {
        battle.StartCoroutine(ExecuteMove());
    }

    private IEnumerator ExecuteMove()
    {
        var player = battle.GetCurrentUnitAs<PlayerUnit>();

        if (player == null)
        {
            Debug.LogError("Something went wrong");
        }

        player.PlayAttack();
        yield return new WaitForSeconds(1f);

        foreach (Unit targetUnit in targetUnits)
        {
            int damageValue = UnityEngine.Random.Range(move.minDamage, move.maxDamage) * player.unitStats.strength;

            targetUnit.TakeDamage(damageValue);
            targetUnit.PlayHit();

            if (targetUnit.IsDead)
            {
                targetUnit.PlayDeath();
                //yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitForSeconds(1.5f);

            battle.OnTurnEnded();
        }
    }
}

