using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum MapNodeType
{
    Fight = 0,
    Food = 1,
    Gas = 2,
    Rest = 3,
    Water = 4
}

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }

    [Header("Map Settings")] public List<GameObject> currentNodes = new List<GameObject>();
    public int FightPercent = 50;
    [SerializeField] private NodeDataScriptableObject mapNodeSO;

    [Header("Map Components")] public GameObject mapLayerOne;
    public GameObject mapLayerTwo;
    public GameObject mapLayerThree;
    [SerializeField] GameObject mapBG;
    [SerializeField] GameObject[] mapNodePrefabs;

    [Header("Event Components")] public GameObject eventWindow;
    public Image eventImage;
    public TextMeshProUGUI eventText;
    public Button[] eventButtons;

    [Header("Resources")] [SerializeField] private int startGas = 10;
    [SerializeField] private int maxGas = 20;
    [SerializeField] private int currentGas;

    [SerializeField] private int startFood = 10;
    [SerializeField] private int maxFood = 20;
    [SerializeField] private int currentFood;
    [SerializeField] private Slider gasSlider;
    [SerializeField] private Slider foodSlider;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        if (PlayerStatsManager.Instance.runStarted)
        {
            // TODO: Load Data
            LoadMapNodesFromScriptableObject();
        }
        else
        {
            // Start Run
            Debug.Log("New Run Started");
            PlayerStatsManager.Instance.runStarted = true;

            SpawnNodes(mapLayerOne, 1);
            SpawnNodes(mapLayerTwo, 2);
            SpawnNodes(mapLayerThree, 3);
            SetNodeActivation();

            // Set Stats
            currentGas = Mathf.Clamp(startGas, 0, maxGas);
            currentFood = Mathf.Clamp(startFood, 0, maxFood);
            UpdateResourceUI();

            // Play Dialogue
            NarrativeHandler.Instance.StartDialogue("Intro");
        }
    }

    public void SetNodeActivation()
    {
        for (int i = 0; i < currentNodes.Count; i++)
        {
            Button btn = currentNodes[i].GetComponent<Button>();

            if (i == 0)
            {
                btn.interactable = false;
            }
            else if (i == 1 || i == 2)
            {
                btn.interactable = true;
            }
            else
            {
                btn.interactable = false;
            }
        }
    }

    public void SpawnNodes(GameObject parentLayer, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            // Choose a random prefab
            int prefabIndex = Random.Range(0, mapNodePrefabs.Length);
            GameObject selectedNode = mapNodePrefabs[prefabIndex];

            GameObject instance = Instantiate(selectedNode, Vector3.zero, Quaternion.identity);
            instance.transform.SetParent(parentLayer.transform, worldPositionStays: false);

            // Add to current nodes
            currentNodes.Add(instance);

            // Assign node index to the MapNode
            MapNode nodeScript = instance.GetComponent<MapNode>();
            if (nodeScript != null)
            {
                nodeScript.nodeIndex = currentNodes.Count - 1; // Use index in the updated list
            }
        }
    }

    public void AdvanceMap(int selectedIndex)
    {
        // Store the new base node (the one that was clicked)
        GameObject newBase = currentNodes[selectedIndex];

        // Determine which nodes will become new 1 and 2
        int next1Index = selectedIndex + 2;
        int next2Index = selectedIndex + 3;

        List<GameObject> newNodes = new List<GameObject>
        {
            newBase
        };

        newNodes.Add(currentNodes[next1Index]);
        newNodes.Add(currentNodes[next2Index]);

        // Destroy all old nodes
        foreach (var node in currentNodes)
        {
            if (!newNodes.Contains(node))
                Destroy(node);
        }

        // Clear and update current list
        currentNodes.Clear();
        currentNodes.AddRange(newNodes);

        // Regenerate new layer
        SpawnNodes(mapLayerThree, 3);

        // Reassign layers based on new node positions
        ReassignNodeLayers();

        // Use Gas
        UseGas(1);

        // Save Data
        //PlayerStatsManager.Instance.currentMapNodes

        // Reactivate buttons
        SetNodeActivation();
    }

    void ReassignNodeLayers()
    {
        for (int i = 0; i < currentNodes.Count; i++)
        {
            Transform targetParent = null;

            if (i == 0)
                targetParent = mapLayerOne.transform;
            else if (i == 1 || i == 2)
                targetParent = mapLayerTwo.transform;
            else
                targetParent = mapLayerThree.transform;

            GameObject node = currentNodes[i];
            node.transform.SetParent(targetParent, worldPositionStays: false);

            // Reassign Indexes
            MapNode nodeScript = node.GetComponent<MapNode>();
            if (nodeScript != null)
            {
                nodeScript.nodeIndex = i;
            }
        }
    }

    public void CreateEvent(Sprite image, string eventDescription, int buttonIndex)
    {
        Debug.Log(buttonIndex);

        // Set Event Image
        eventImage.sprite = image;

        // Set Event Text
        eventText.text = eventDescription;

        // Set Buttons
        for (int i = 0; i < eventButtons.Length; i++)
        {
            eventButtons[i].gameObject.SetActive(false);
        }

        eventButtons[buttonIndex].gameObject.SetActive(true);
        eventWindow.SetActive(true);
    }

    public void CloseEvent()
    {
        eventWindow.SetActive(false);
    }

    public void EnterRest()
    {
        UseFood(5);
        NarrativeHandler.Instance.LoadRestDialogue();
        CloseEvent();
    }

    public void EnterFight()
    {
        // TODO
        SaveMapNodesToScriptableObject();
        SceneManager.LoadScene("BattleScene");
    }

    public void UseGas(int amount)
    {
        currentGas = Mathf.Max(currentGas - amount, 0);
        UpdateResourceUI();
    }

    public void UseFood(int amount)
    {
        currentFood = Mathf.Max(currentFood - amount, 0);
        UpdateResourceUI();
    }


    public void UpdateResourceUI()
    {
        gasSlider.maxValue = maxGas;
        gasSlider.value = currentGas;

        foodSlider.maxValue = maxFood;
        foodSlider.value = currentFood;
    }

    //Writes nodes to scriptable object as a list of ints
    void SaveMapNodesToScriptableObject()
    {
        mapNodeSO.savedNodes = ConvertNodesToIntList();
    }

    //Loads nodes from scriptable object and instantiates them
    //Make sure that the indices in the enum are the same as the prefabs!!
    void LoadMapNodesFromScriptableObject()
    {
        foreach (MapNodeType type in mapNodeSO.savedNodes)
        {
            GameObject prefab = mapNodePrefabs[(int)type]; // mapNodePrefabs must match enum order
            
            //TODO: Replace this block with the spawning logic you want to implement
            GameObject instance = Instantiate(prefab);
            currentNodes.Add(instance);
        }
    }

    //Converts currentNodes to a list of ints based on MapNode enum
    //If you want to change the indices of each mapnode change it in the MapNode enum at the top of this script
    //Make sure that the indices in the enum are the same as the prefabs!!
    public List<int> ConvertNodesToIntList()
    {
        List<int> nodeTypeList = new List<int>();

        foreach (GameObject node in currentNodes)
        {
            MapNode nodeScript = node.GetComponent<MapNode>();
            if (nodeScript != null)
            {
                nodeTypeList.Add((int)nodeScript.nodeType);
            }
            else
            {
                Debug.LogWarning("Node is missing MapNode component.");
            }
        }

        return nodeTypeList;
    }
}