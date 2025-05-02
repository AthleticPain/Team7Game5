using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffActions : MonoBehaviour
{
    [SerializeField] private BattleSystem battle;
    [SerializeField] private PlayerUnit[] playerUnits;

    private void Start()
    {
        playerUnits = battle.GetListOfUnits<PlayerUnit>();
    }

    public void Protect(int moveIndex)
    {
        PlayerUnit currentUnit = battle.GetCurrentUnitAs<PlayerUnit>();
        if (currentUnit == null)
        {
            Debug.LogError("Something went wrong");
        }

        Unit[] targetUnits = new Unit[1];
        targetUnits[0] = currentUnit == playerUnits[0] ? playerUnits[1] : playerUnits[0];
        
        //TODO: Remove hardcoded values and make it dynamic
        currentUnit.selectedMoveIndex = moveIndex;
        MoveSO selectedMove = currentUnit.Moves[currentUnit.selectedMoveIndex];
        
        battle.OnPlayerTargetConfirmed(targetUnits, selectedMove);
    }
}