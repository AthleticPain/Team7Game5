using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetSelectionState : BattleStateBase
{
    private BattleUIManager uiManager;
    private List<EnemyUnit> enemies;
    private List<PlayerUnit> players;
    private EnemyUnit selectedTarget;
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
        selectedTarget = enemies[0];
        currentPlayer = battle.GetCurrentUnitAs<PlayerUnit>();

        uiManager.SetDimBackground(true);

        DimNonTargetedEnemies();

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

        DimNonTargetedEnemies();

        foreach (var player in players)
        {
            if (player == currentPlayer) continue;
            player.unitVisuals.SetDimOverlays(true);
        }
    }

    private void DimNonTargetedEnemies()
    {
        foreach (var enemy in enemies)
        {
            enemy.unitVisuals.SetDimOverlays(enemy != selectedTarget);
        }
    }

    public override void Exit()
    {
        uiManager.SetDimBackground(false);

        foreach (EnemyUnit enemy in enemies)
        {
            enemy.unitVisuals.SetDimOverlays(false);
            enemy.unitVisuals.OnPointerEntered.RemoveAllListeners();
        }

        foreach (var player in players)
        {
            player.unitVisuals.SetDimOverlays(false);
        }
    }

    private void ChangeTarget(EnemyUnit targetedEnemy)
    {
        Debug.Log($"Changing enemy to {targetedEnemy}");
        selectedTarget = targetedEnemy;
        DimNonTargetedEnemies();
    }

    private void ConfirmTarget(EnemyUnit targetEnemy)
    {
        battle.OnPlayerTargetConfirmed(targetEnemy, currentlySelectedMove);
    }
}