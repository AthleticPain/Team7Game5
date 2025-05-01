using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActions : MonoBehaviour
{
    [SerializeField] private BattleSystem battle;

    //0->Medkit
    //1->Molotov
    //2->Water Bottle
    [SerializeField] private MoveSO[] itemMoves;
    
    public void MedKit()
    {
        PlayerUnit[] targets = { battle.GetCurrentUnitAs<PlayerUnit>() };
        
        battle.OnPlayerTargetConfirmed(targets, itemMoves[0]);
    }
    
    public void Molotov()
    {
        EnemyUnit[] targets = battle.GetListOfUnits<EnemyUnit>();

        Debug.Log($"Molotov Targets ({targets.Length}): ");
        foreach (var unit in targets)
        {
            Debug.Log(unit.gameObject.name);
        }

        battle.OnPlayerTargetConfirmed(targets, itemMoves[1]);
    }

    public void WaterBottle()
    {
        battle.OnPlayerMoveSelected(2);
    }
}