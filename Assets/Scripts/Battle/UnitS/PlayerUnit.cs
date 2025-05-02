using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public int selectedMoveIndex;
    public List<MoveSO> Moves => moves;
    

    public void PlayHighlightAnimation()
    {
        //Play Highlight Animation Here
        unitVisuals?.PlayHighlightTurn();
    }

    public override void PlayHeal()
    {
        base.PlayHeal();
        unitVisuals?.PlayHeal();
    }

    // public override void PlayHit()
    // {
    //     //Play hit animation here
    //     Debug.Log("Playing player taking damage animation");
    //     //throw new System.NotImplementedException();
    // }
    //
    // public override void PlayDeath()
    // {
    //     //Play death animation here
    //     Debug.Log("Playing player death animation");
    //     //throw new System.NotImplementedException();
    // }
    //
    // public override void PlayAttack()
    // {
    //     //Play attack animation
    //     Debug.Log("Playing player attacking animations");
    // }
}