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
        switch(nodeType)
        {
            case MapNodeType.Gas:
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 0);
                break;
            case MapNodeType.Food:
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 0);
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
