using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionSelectionState : BattleStateBase
{
    public PlayerActionSelectionState(BattleSystem battle) : base(battle) { }

    public override void Enter()
    {
        Debug.Log("Player action selection started.");
        
        //Show battle UI here
        battle.ShowActionMenu();
    }

    public override void Update()
    {
        // Wait for the player to choose an action, then call battle.OnPlayerChosenMove()
    }

    public override void Exit()
    {
        Debug.Log("Player action selection ended.");
    }
}
