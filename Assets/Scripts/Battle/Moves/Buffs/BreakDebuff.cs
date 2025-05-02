using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMove", menuName = "Moves/Debuffs/Break")]
public class BreakDebuff : Buff
{
    public override IEnumerator ExecuteMove(Unit moveUser, Unit[] targetUnits)
    {
        base.ExecuteMove(moveUser, targetUnits);

        foreach (Unit target in targetUnits)
        {
            target.IsVulnerable = true;
        }
        
        moveUser.PlayAttack();
        yield return new WaitForSeconds(0.2f);
        
    }
}
