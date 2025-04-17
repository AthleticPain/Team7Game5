using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    public int nodeIndex;
    public string nodeType;
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
            case "Gas":
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 0);
                break;
            case "Food":
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 0);
                break;
            case "Fight":
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 1);
                break;
            case "Rest":
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 2);
                break;
            case "Water":
                MapManager.Instance.CreateEvent(eventImage, eventDescription, 0);
                break;
        }
    }
}
