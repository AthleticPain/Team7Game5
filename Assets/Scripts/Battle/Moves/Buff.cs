using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// [CreateAssetMenu(fileName = "NewMove", menuName = "Moves/Buff")]
public class Buff : MoveSO
{
    public override IEnumerator ExecuteMove(Unit moveUser, Unit[] targetUnits)
    {
        Debug.Log($"{moveUser.name} casting {moveName} buff on:");

        foreach (Unit targetUnit in targetUnits)
        {
            Debug.Log(targetUnit.name);
        }
        
        yield return null;
    }
}
