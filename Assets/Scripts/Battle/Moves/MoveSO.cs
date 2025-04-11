using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMove", menuName = "Battle/Move")]
public class MoveSO : ScriptableObject
{
    public string moveName;
    public int power;
}