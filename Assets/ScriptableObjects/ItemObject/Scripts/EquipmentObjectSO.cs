using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Equipment Object", menuName = "ScriptableObjects/Items/EquipmentObject", order = 2)]
public class EquipmentObjectSO : AbsctractItemObjectSO
{
    public void Awake()
    {
        Type = ItemObjectType.Consumable;
    }
}
