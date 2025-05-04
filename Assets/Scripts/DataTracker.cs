using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataTracker : MonoBehaviour
{
    /*
        Fight = 0,
        Food = 1,
        Gas = 2,
        Rest = 3,
        Water = 4
    */

    public static DataTracker Instance { get; private set; }
    public TravelData currentRun;

    private string mapNodePath;
    public MapNodeReport mapNodeReport = new MapNodeReport();

    public TravelDataLog travelDataLog = new TravelDataLog();
    
    private string travelDataPath;

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

    private void Start()
    {
        travelDataPath = Path.Combine(Application.persistentDataPath, "RawTravelData.json");
        Debug.Log("Saving RawTravelData.json to: " + travelDataPath);

        mapNodePath = Path.Combine(Application.persistentDataPath, "MapNodeReport.json");
        Debug.Log("Saving MapNodeReport.json to: " + mapNodePath);
        

        LoadTravelData();

        if (File.Exists(travelDataPath))
        {
            string json = File.ReadAllText(travelDataPath);
            travelDataLog = JsonUtility.FromJson<TravelDataLog>(json);
        }
        else
        {
            travelDataLog = new TravelDataLog();
        }
    }

    private void LoadTravelData()
    {
        if (File.Exists(mapNodePath))
        {
            string json = File.ReadAllText(mapNodePath);
            mapNodeReport = JsonUtility.FromJson<MapNodeReport>(json);
        }
        else
        {
            mapNodeReport = new MapNodeReport();
            SaveMapNodeReport(); // Create initial file
        }
    }


    public void StartNewRun()
    {
        currentRun = new TravelData();
    }


    public void RecordNodeVisit(int nodeType)
    {
        if (currentRun == null)
        {
            StartNewRun(); // Ensure there's a run started
        }

        currentRun.nodeVisitSequence.Add(nodeType);


        switch (nodeType)
        {
            case 0: 
                mapNodeReport.Fight++;
                break;
            case 1: 
                mapNodeReport.Food++; 
                break;
            case 2: 
                mapNodeReport.Gas++; 
                break;
            case 3: 
                mapNodeReport.Rest++; 
                break;
            case 4: 
                mapNodeReport.Water++; 
                break;
            default: Debug.LogWarning("Unknown node type: " + nodeType); break;
        }

        SaveRunLog();
        SaveMapNodeReport();
    }

    private void SaveRunLog()
    {
        string json = JsonUtility.ToJson(travelDataLog, true);
        File.WriteAllText(travelDataPath, json);
    }

    private void SaveMapNodeReport()
    {
        string json = JsonUtility.ToJson(mapNodeReport, true);
        File.WriteAllText(mapNodePath, json);
    }

    public void EndRun()
    {
        if (currentRun != null)
        {
            travelDataLog.runs.Add(currentRun);
            SaveRunLog();
            currentRun = null;
        }
    }
}
