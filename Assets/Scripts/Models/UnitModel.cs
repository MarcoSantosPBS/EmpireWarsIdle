using System;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitModel", menuName = "Units/UnitModel")]
public class UnitModel : ScriptableObject
{
    public ResourceEnum Unit;
    public int LootCapacity;
    public int Defense;
    public int Attack;
}

