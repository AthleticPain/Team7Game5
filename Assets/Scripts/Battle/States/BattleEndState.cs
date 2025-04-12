using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEndState : BattleStateBase
{
    public BattleEndState(BattleSystem battle) : base(battle) { }

    public override void Enter()
    {
        Debug.Log("Battle ended!");
        //battle.ShowEndScreen();
    }
}
