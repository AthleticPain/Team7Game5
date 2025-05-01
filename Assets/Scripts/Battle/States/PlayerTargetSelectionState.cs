using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetSelectionState : BattleStateBase
{
    private BattleUIManager uiManager;
    private List<EnemyUnit> enemies;
    private List<PlayerUnit> players;
    private EnemyUnit selectedEnemyTarget;
    private PlayerUnit currentPlayer;
    private MoveSO currentlySelectedMove;

    public PlayerTargetSelectionState(BattleSystem battle, BattleUIManager uiManager, List<EnemyUnit> enemies,
        List<PlayerUnit> players, MoveSO currentlySelectedMove) : base(battle)
    {
        this.uiManager = uiManager;
        this.enemies = enemies;
        this.players = players;
        this.currentlySelectedMove = currentlySelectedMove;
    }

    public override void Enter()
    {
        //Target first enemy that isn't dead by default
        foreach (EnemyUnit enemyUnit in enemies)
        {
            if (!enemyUnit.IsDead)
            {
                selectedEnemyTarget = enemyUnit;
                break;
            }
        }

        currentPlayer = battle.GetCurrentUnitAs<PlayerUnit>();

        uiManager.SetDimBackground(true);

        UpdateEnemyVisuals();

        // for (int i = 0; i < enemies.Count; i++)
        // {
        //     EnemyUnit constEnemy = enemies[i];
        //     Debug.Log($"Subscribing to event for {enemies[i].name}");
        //     enemies[i].unitVisuals.OnPointerEntered.AddListener(() => ChangeTarget(constEnemy));
        // }

        foreach (EnemyUnit enemy in enemies)
        {
            Debug.Log($"Subscribing to event for {enemy}");
            enemy.unitVisuals.OnPointerEntered.AddListener(() => ChangeTarget(enemy));
            enemy.unitVisuals.OnPointerClicked.AddListener(() => ConfirmTarget(enemy));
        }

        UpdateEnemyVisuals();

        foreach (var player in players)
        {
            if (player == currentPlayer) continue;
            player.unitVisuals.SetVisualState(UnitVisuals.VisualState.dimmed);
        }
    }

    private void UpdateEnemyVisuals()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == selectedEnemyTarget)
            {
                enemy.unitVisuals.SetVisualState(UnitVisuals.VisualState.targeted);
            }
            else
            {
                enemy.unitVisuals.SetVisualState(UnitVisuals.VisualState.dimmed);
            }
        }
    }

    public override void Exit()
    {
        uiManager.SetDimBackground(false);

        foreach (EnemyUnit enemy in enemies)
        {
            enemy.unitVisuals.SetVisualState(UnitVisuals.VisualState.normal);
            enemy.unitVisuals.OnPointerEntered.RemoveAllListeners();
            enemy.unitVisuals.OnPointerClicked.RemoveAllListeners();
        }

        foreach (var player in players)
        {
            player.unitVisuals.SetVisualState(UnitVisuals.VisualState.normal);
        }
    }

    private void ChangeTarget(EnemyUnit targetedEnemy)
    {
        if (!targetedEnemy.IsDead)
        {
            Debug.Log($"Changing enemy to {targetedEnemy}");
            selectedEnemyTarget = targetedEnemy;
            UpdateEnemyVisuals();
        }
    }

    private void ConfirmTarget(EnemyUnit targetEnemy)
    {
        if (!targetEnemy.IsDead)
        {
            Unit[] targetEnemies = { targetEnemy };
            
            Debug.Log($"Target confirmed. Using Move: {currentlySelectedMove.name}.");
            battle.OnPlayerTargetConfirmed(targetEnemies, currentlySelectedMove);
        }
    }
}