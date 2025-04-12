using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : Unit
{
    public void PlayHit()
    {
        //Play hit animation here
        Debug.Log("Playing player taking damage animation");
        //throw new System.NotImplementedException();
    }

    public void PlayDeath()
    {
        //Play death animation here
        Debug.Log("Playing player death animation");
        //throw new System.NotImplementedException();
    }
}
