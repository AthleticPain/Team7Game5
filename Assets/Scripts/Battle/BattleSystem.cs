using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleSystem : MonoBehaviour
{
    private BattleStateBase currentState;

    [SerializeField] List<EnemyUnit> enemyUnits;
    [SerializeField] List<PlayerUnit> playerUnits;
    [SerializeField] List<Unit> unitsInTurnOrder;
    
    [Header("Debug")]
    [SerializeField, ReadOnly] private string currentStateName;
    [SerializeField, ReadOnly] private int currentTurnIndex;

    //Set a new state
    public void SetState(BattleStateBase newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
        
        currentStateName = currentState.GetType().Name;
    }

    private void Start()
    {
        currentTurnIndex = -1;
        OnTurnEnded(); //Increments turn index and sets state according to player or enemy turn
    }

    //Call the update function of current state
    private void Update()
    {
        currentState?.Update();
    }

    public void OnPlayerTurn()
    {
        SetState(new PlayerActionSelectionState(this));
    }

    public void OnPlayerChosenMove(MoveSO move)
    {
        SetState(new PlayerMoveExecutionState(this, move));
    }

    public void OnEnemyTurn()
    {
        SetState(new EnemyTurnState(this));
    }

    public void EndBattle()
    {
        //SetState(new BattleEndState(this));
    }

    //Called when player or enemy ends their turn
    public void OnTurnEnded()
    {
        currentTurnIndex = (currentTurnIndex + 1) % unitsInTurnOrder.Count;
        
        var currentUnit = unitsInTurnOrder[currentTurnIndex];
        
        if (currentUnit is PlayerUnit)
            OnPlayerTurn();
        else
            OnEnemyTurn();
    }

    public void ShowActionMenu()
    {
        //Player action UI logic here
    }

    public EnemyUnit GetCurrentEnemyUnit()
    {
        if (unitsInTurnOrder[currentTurnIndex] is EnemyUnit)
        {
            return (EnemyUnit)unitsInTurnOrder[currentTurnIndex];
        }
        else
        {
            return null;
        }
    }

    public PlayerUnit GetRandomLivingPlayer()
    {
        var livingPlayers = playerUnits.Where(p => p.unitState != UnitStateEnum.dead).ToList();

        if (livingPlayers.Count == 0)
        {
            Debug.LogWarning("No living players left!");
            return null; // Or handle game over
        }

        int index = Random.Range(0, livingPlayers.Count);
        return livingPlayers[index];
    }
}