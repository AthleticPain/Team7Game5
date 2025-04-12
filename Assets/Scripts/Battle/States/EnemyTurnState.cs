using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : BattleStateBase
{
    public EnemyTurnState(BattleSystem battle) : base(battle) { }

    public override void Enter()
    {
        battle.StartCoroutine(EnemyTurn());
    }

    private IEnumerator EnemyTurn()
    {
        var enemy = battle.GetCurrentUnitAs<EnemyUnit>();

        if (enemy == null)
        {
            Debug.LogError("Something went wrong");
        }
        
        var playerTarget = battle.GetRandomLivingPlayer();
        var move = enemy.GetRandomMove();

        enemy.PlayAttack();
        yield return new WaitForSeconds(1f);

        playerTarget.TakeDamage(move.power);
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

