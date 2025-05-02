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

    [SerializeField] private BattleUIManager battleUIManager;
    [SerializeField] List<EnemyUnit> enemyUnits;
    [SerializeField] List<PlayerUnit> playerUnits;
    [SerializeField] List<Unit> unitsInTurnOrder;

    [Header("Debug")] [SerializeField, ReadOnly]
    private string currentStateName;

    [SerializeField, ReadOnly] private int currentTurnIndex;
    public Unit CurrentUnit => unitsInTurnOrder[currentTurnIndex];

    //Set a new state
    public void SetState(BattleStateBase newState)
    {
        Debug.Log($"*** Exiting {currentState?.GetType().Name} and entering {newState.GetType().Name} ***");

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();

        if (!(currentState is PlayerActionSelectionState))
        {
            battleUIManager.HidePlayerActionsPanel();
        }

        currentStateName = currentState.GetType().Name;
    }

    private void Start()
    {
        //Set Enemy Stats
        foreach (Unit unit in unitsInTurnOrder)
        {
            if (unit is EnemyUnit)
                unit.unitStats.ResetStats();

            unit.InitializeUnit();
        }

        currentTurnIndex = -1;
        Invoke(nameof(OnTurnEnded), 1); //Increments turn index and sets state according to player or enemy turn
        //OnTurnEnded(); 
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

    //When player selects their move, move to target selection state
    public void OnPlayerMoveSelected(int moveIndex)
    {
        if (currentState is PlayerActionSelectionState)
        {
            PlayerUnit currentPlayerUnit = unitsInTurnOrder[currentTurnIndex] as PlayerUnit;
            currentPlayerUnit.selectedMoveIndex = moveIndex;

            Debug.Log("Selected move: " + currentPlayerUnit.Moves[currentPlayerUnit.selectedMoveIndex].name);

            MoveSO selectedMove = currentPlayerUnit.Moves[currentPlayerUnit.selectedMoveIndex];

            Unit[] targetUnits;
            switch (selectedMove.TargetingType)
            {
                case TargetingType.singleEnemy:
                    SetState(new PlayerTargetSelectionState(this, battleUIManager, enemyUnits, playerUnits,
                        selectedMove));
                    break;
                case TargetingType.allEnemies:
                    SetState(new PlayerMoveExecutionState(this, selectedMove, enemyUnits.ToArray()));
                    break;
                case TargetingType.randomEnemy:
                    Unit randomTarget = enemyUnits[Random.Range(0, enemyUnits.Count)];
                    SetState(new PlayerMoveExecutionState(this, selectedMove, new Unit[] { randomTarget }));
                    break;
                case TargetingType.friendly:
                    targetUnits = new Unit[1];
                    targetUnits[0] = currentPlayerUnit == playerUnits[0] ? playerUnits[1] : playerUnits[0];
                    SetState(new PlayerMoveExecutionState(this, selectedMove, targetUnits));
                    break;
                case TargetingType.self:
                    targetUnits = new Unit[] { currentPlayerUnit };
                    SetState(new PlayerMoveExecutionState(this, selectedMove, targetUnits));
                    break;
                default:
                    break;
            }
        }
    }

    public void OnPlayerMoveSelected(MoveSO selectedMove)
    {
        if (currentState is PlayerActionSelectionState)
        {
            Debug.Log("Selected move: " + selectedMove.name);

            SetState(new PlayerTargetSelectionState(this, battleUIManager, enemyUnits, playerUnits, selectedMove));
        }
    }

    public void OnPlayerTargetConfirmed(Unit[] targetUnits, MoveSO selectedMove)
    {
        Debug.Log("Player target Confirmed. Current State: " + currentState.GetType().Name);

        //Only allow this to happen when it is player's turn
        if (currentState is PlayerTargetSelectionState || currentState is PlayerActionSelectionState)
        {
            Debug.Log("Changing State to PlayerMoveExecutionState. Selected move: " + selectedMove.name);
            SetState(new PlayerMoveExecutionState(this, selectedMove, targetUnits));
        }
        else
        {
            Debug.LogError("Oops. This should never happen.");
        }
    }

    public void OnEnemyTurn()
    {
        SetState(new EnemyTurnState(this));
    }

    public void EndBattle(bool win)
    {
        string endingStatement = win ? "You win!" : "You lose!";
        Debug.Log("Battle Ended. " + endingStatement);
        SetState(new BattleEndState(this));

        if (win)
        {
            foreach (PlayerUnit playerUnit in playerUnits)
            {
                playerUnit.unitStats.IncreaseStats(1);
                playerUnit.unitStats.currentLevel++;
            }
        }

        battleUIManager.ShowGameOverPanel(win);
    }

    //Called when player or enemy ends their turn
    public void OnTurnEnded()
    {
        //Check if game over conditions are met
        if (IsBattleOver())
            return;

        //If game is not over, find next living unit's turn
        AdvanceToNextLivingUnit();
        var currentUnit = unitsInTurnOrder[currentTurnIndex];
        Debug.Log(currentUnit.name + "'s Turn!");

        if (currentUnit is PlayerUnit)
            OnPlayerTurn();
        else
            OnEnemyTurn();
    }

    //Checks if either all players are dead or all enemies are dead and ends battle accordingly
    private bool IsBattleOver()
    {
        bool allPlayersDead = playerUnits.All(p => p.IsDead);
        if (allPlayersDead)
        {
            EndBattle(false); // Lost
            return true;
        }

        bool allEnemiesDead = enemyUnits.All(e => e.IsDead);
        if (allEnemiesDead)
        {
            EndBattle(true); // Won
            return true;
        }

        return false;
    }

    //Increments currentTurnIndex global variable to next living unit
    private void AdvanceToNextLivingUnit()
    {
        int startingIndex = currentTurnIndex;
        do
        {
            currentTurnIndex = (currentTurnIndex + 1) % unitsInTurnOrder.Count;

            if (!unitsInTurnOrder[currentTurnIndex].IsDead)
                return;
        } while (currentTurnIndex != startingIndex);

        // Failsafe: if we somehow loop all units and find all dead (shouldn’t happen due to IsBattleOver)
        Debug.LogError("No living units found when advancing turn.");
    }

    public void ShowActionMenu()
    {
        //Player action UI logic here
        battleUIManager.ShowPlayerActionsPanel();
    }

    public void HighlightPlayerUnit()
    {
        GetCurrentUnitAs<PlayerUnit>().PlayHighlightAnimation();
    }

    //Returns the current unit as type T
    public T GetCurrentUnitAs<T>() where T : Unit
    {
        return unitsInTurnOrder[currentTurnIndex] as T;
    }

    public T[] GetListOfUnits<T>() where T : Unit
    {
        T[] units = unitsInTurnOrder.FindAll(x => x is T).Cast<T>().ToArray();
        return units;
    }

    public PlayerUnit GetRandomLivingPlayer()
    {
        var livingPlayers = playerUnits.Where(p => p.unitState != UnitStateEnum.dead).ToList();

        //Redundancy check, ideally code should never go into this block
        if (livingPlayers.Count == 0)
        {
            Debug.LogWarning("Something went Wrong! No living players left!");
            return null; // Or handle game over
        }

        int index = Random.Range(0, livingPlayers.Count);
        return livingPlayers[index];
    }
}