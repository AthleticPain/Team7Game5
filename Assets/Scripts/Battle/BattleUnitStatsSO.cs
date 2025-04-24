using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitStatsSO", menuName = "new UnitStatsSO")]
public class BattleUnitStatsSO : ScriptableObject
{
    public int hp;
    public int strength;
    public int speed;
    public int critChance;
}
