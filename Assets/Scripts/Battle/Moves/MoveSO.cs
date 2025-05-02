using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "NewMove", menuName = "Moves/ParentMove")]
[Serializable]
public abstract class MoveSO : ScriptableObject
{
    public string moveName;

    public abstract IEnumerator ExecuteMove(Unit moveUser, Unit[] targetUnits);
    
}