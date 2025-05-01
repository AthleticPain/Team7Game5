using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsDisplay : MonoBehaviour
{
    [SerializeField] private BattleUnitStatsSO playerStats;
    [SerializeField] private TMP_Text statsText;

    private void OnEnable()
    {
        statsText.text =
            $"Level: {playerStats.currentLevel}\n" +
            $"Strength: {playerStats.strength}\n" +
            $"Speed: {playerStats.speed}\n";
    }
}
