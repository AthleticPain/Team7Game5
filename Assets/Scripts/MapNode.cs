using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapNode : MonoBehaviour
{
    public string nodeType;
    public int nodeIndex;

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
        MapManager.Instance.AdvanceMap(nodeIndex);
    }
}
