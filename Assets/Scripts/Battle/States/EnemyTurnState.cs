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

        float scalingFactor = 1;
        switch (move.scalingStat)
        {
            case StatToScaleWith.Strength:
                scalingFactor = enemy.unitStats.strength;
                break;
            case StatToScaleWith.Speed:
                scalingFactor = enemy.unitStats.speed;
                break;
            default:
                break;
        }
        
        int damageValue = move.GetDamageValue(scalingFactor);

        playerTarget.TakeDamage(damageValue);
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

