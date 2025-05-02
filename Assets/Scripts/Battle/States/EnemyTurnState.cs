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

        //Assuming the move is going to be an attack
        Attack move = enemy.GetRandomMove() as Attack;

        if (move != null)
        {
            var playerTarget = new Unit[] { battle.GetRandomLivingPlayer() };
            
            yield return move.ExecuteMove(enemy, playerTarget);
            
            yield return new WaitForSeconds(1.5f);
        }
        else
        {
            Debug.LogError("Something went wrong");
        }

        // Loop back to next player
        battle.OnTurnEnded();
    }
}