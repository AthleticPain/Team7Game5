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
        Debug.Log($"Setting this move to {move.name}");
        this.targetUnits = targetUnits;
    }

    public override void Enter()
    {
        battle.StartCoroutine(ExecuteMove());
    }

    private IEnumerator ExecuteMove()
    {
        var player = battle.GetCurrentUnitAs<PlayerUnit>();
        int damageValue = move.GetDamageValue(player.unitStats);

        if (player == null)
        {
            Debug.LogError("Something went wrong");
        }

        if (damageValue >= 0)
        {
            player.PlayAttack();
            yield return new WaitForSeconds(1f);
        }

        foreach (Unit targetUnit in targetUnits)
        {
            Debug.Log($"{player.name} is attacking {targetUnit.name} with {move.name}.");


            targetUnit.TakeDamage(damageValue);
            if (damageValue > 0)
                targetUnit.PlayHit();
            else
                targetUnit.PlayHeal();

            if (targetUnit.IsDead)
            {
                targetUnit.PlayDeath();
                //yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(1.5f);

        battle.OnTurnEnded();
    }
}