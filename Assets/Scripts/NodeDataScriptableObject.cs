using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "NewMapSO", menuName = "NewMapSO", order = 1)]
public class NodeDataScriptableObject : ScriptableObject
{
    public List<int> savedNodes = new List<int>();
}
