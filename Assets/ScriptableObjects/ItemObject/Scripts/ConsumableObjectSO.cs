using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Consumable Object", menuName = "ScriptableObjects/Items/ConsumableObject", order = 2)]
public class ConsumableObjectSO : AbsctractItemObjectSO
{
    public int RestoreHealthValue;

    public void Awake()
    {
        Type = ItemObjectType.Consumable;
    }
}
