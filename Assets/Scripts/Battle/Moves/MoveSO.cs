using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetingType
{
    singleEnemy,
    allEnemies,
    randomEnemy,
    friendly,
    self
}

//[CreateAssetMenu(fileName = "NewMove", menuName = "Moves/ParentMove")]
[Serializable]
public abstract class MoveSO : ScriptableObject
{
    public string moveName;

    public abstract IEnumerator ExecuteMove(Unit moveUser, Unit[] targetUnits);
    [SerializeField] private TargetingType targetingType = TargetingType.singleEnemy;
    public TargetingType TargetingType => targetingType;
}