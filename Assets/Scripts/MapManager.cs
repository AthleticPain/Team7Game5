using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }


    [Header("Map Settings")]
    public List<GameObject> currentNodes = new List<GameObject>();
    public int FightPercent = 50;

    [Header("Map Components")]
    public GameObject mapLayerOne;
    public GameObject mapLayerTwo;
    public GameObject mapLayerThree;
    [SerializeField] GameObject mapBG;
    [SerializeField] GameObject[] mapNodePrefabs;

    [Header("Event Components")]
    public GameObject eventWindow;
    public Image eventImage;
    public TextMeshProUGUI eventText;
    public Button[] eventButtons;

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
        SpawnNodes(mapLayerOne, 1);
        SpawnNodes(mapLayerTwo, 2);
        SpawnNodes(mapLayerThree, 3);
        SetNodeActivation();
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

        // Save Data
        

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
        // TODO
    }

    public void EnterFight()
    {
        // TODO
        SceneManager.LoadScene("BattleScene");
    }
}
