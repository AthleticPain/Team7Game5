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

    public void Molotov()
    {
        EnemyUnit[] targets = battle.GetListOFUnits<EnemyUnit>();

        Debug.Log($"Molotov Targets ({targets.Length}): ");
        foreach (var unit in targets)
        {
            Debug.Log(unit.gameObject.name);
        }
        
        battle.OnPlayerTargetConfirmed(targets, itemMoves[1]);
    }
    
}
