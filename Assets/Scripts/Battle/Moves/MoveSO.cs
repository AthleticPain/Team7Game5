using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMove", menuName = "Battle/Move")]
[Serializable]
public class MoveSO : ScriptableObject
{
    public string moveName;
    public int power;
}