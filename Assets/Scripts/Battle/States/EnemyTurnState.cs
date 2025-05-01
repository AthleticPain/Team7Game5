using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : BattleStateBase
{
    public EnemyTurnState(BattleSystem battle) : base(battle)
    {
    }

    public override void Enter()
    {
        battle.StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        var enemy = battle.GetCurrentUnitAs<EnemyUnit>();
        var move = enemy.GetRandomMove();
        int damageValue = move.GetDamageValue(enemy.unitStats);

        if (enemy == null)
        {
            Debug.LogError("Something went wrong");
        }

        var playerTarget = battle.GetRandomLivingPlayer();

        if (damageValue >= 0)
        {
            enemy.PlayAttack();
            yield return new WaitForSeconds(1f);
        }

        playerTarget.TakeDamage(damageValue);
        if (damageValue > 0)
            playerTarget.PlayHit();

        yield return new WaitForSeconds(1.5f);

        if (playerTarget.IsDead)
        {
            playerTarget.PlayDeath();
            yield return new WaitForSeconds(0.5f);
        }

        // Loop back to next player
        battle.OnTurnEnded();
    }
}