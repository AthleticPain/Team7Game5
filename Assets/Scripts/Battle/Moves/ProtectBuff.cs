using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMove", menuName = "Moves/Buffs/Protect")]
public class ProtectBuff : Buff
{
    public override IEnumerator ExecuteMove(Unit moveUser, Unit[] targetUnits)
    {
        base.ExecuteMove(moveUser, targetUnits);

        foreach (Unit target in targetUnits)
        {
            target.isProtected = true;
        }
        
        yield return new WaitForSeconds(1);
    }
}
