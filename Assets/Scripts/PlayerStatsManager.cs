using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance { get; private set; }

    [Header("Map Data")]
    [SerializeField] public NodeDataScriptableObject mapNodeSO;
    [SerializeField] public int nodesTraveled;

    [Header("Character Stats")]
    [SerializeField] public int junoCurrentHP;
    [SerializeField] public int axelCurrentHP;
    [Header("Game Stats")]
    // 0 - Not Started
    // 1 - Travel
    // 2 - Fight
    [SerializeField] public int gameState = 0; 
    [SerializeField] public int currentGas;
    [SerializeField] public int currentFood;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SavePlayerData()
    {
        Debug.Log("Saving Player Data...");

        // Map Data
        PlayerPrefs.SetInt("NodesTraveled", nodesTraveled);
        PlayerPrefs.SetInt("CurrentGas", currentGas);
        PlayerPrefs.SetInt("CurrentFood", currentFood);

        // Character Stats
        PlayerPrefs.SetInt("JunoHP", junoCurrentHP);
        PlayerPrefs.SetInt("AxelHP", axelCurrentHP);

        PlayerPrefs.Save();
    }

    public void LoadPlayerData()
    {
        Debug.Log("Loading Player Data...");
    }
}
