using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance { get; private set; }

    [Header("Map Data")]
    public MapNode[] currentMapNodes;
    public int nodesTraveled;
    public string[] restDialogue;
    public int currentGas;
    public int currentFood;

    [Header("Character Stats")]
    public int junoCurrentHP;
    public int axelCurrentHP;

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
}
