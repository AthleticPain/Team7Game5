using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    public int nodeIndex;
    public MapNodeType nodeType;
    public Sprite eventImage;
    public string eventDescription;
    public string eventDescriptionAlt;

    void Start()
    {
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnNodeClicked);
        }
    }

    void OnNodeClicked()
    {
        ProcessNode();
        MapManager.Instance.AdvanceMap(nodeIndex);
    }

    private void ProcessNode()
    {
        int fightChance = Random.Range(1, 101); // If Higher than Fight Percent then No Fight
        int gainAmount = Random.Range(1, 15); // Gain Amount
        switch(nodeType)
        {
            case MapNodeType.Gas:
                MapManager.Instance.UseGas(-gainAmount);
                Debug.Log("Gain Amount: " + gainAmount);
                if(fightChance > MapManager.Instance.FightPercent)
                {
                    MapManager.Instance.CreateEvent(eventImage, eventDescription, 0);
                }else
                {
                    MapManager.Instance.CreateEvent(eventImage, eventDescriptionAlt, 1);
                }
                break;
            case MapNodeType.Food:
                MapManager.Instance.UseFood(-gainAmount);
                Debug.Log("Gain Amount: " + gainAmount);
                if(fightChance > MapManager.Instance.FightPercent)
                {
                    MapManager.Instance.CreateEvent(eventImage, eventDescription, 0);
                }else
                {
                    MapManager.Instance.CreateEvent(eventImage, eventDescriptionAlt, 1);
                }
                break;
            case MapNodeType.Fight:
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 1);
                break;
            case MapNodeType.Rest:
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 2);
                break;
            case MapNodeType.Water:
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 0);
                break;
        }
    }
}
