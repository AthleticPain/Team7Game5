using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base abstract class for all battle states
public abstract class BattleStateBase
{
    protected BattleSystem battle;

    public BattleStateBase(BattleSystem battle)
    {
        this.battle = battle;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
