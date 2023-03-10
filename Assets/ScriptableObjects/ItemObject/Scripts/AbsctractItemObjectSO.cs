using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemObjectType
{
    Ammo,
    Consumable,
    Equipment
}

public abstract class AbsctractItemObjectSO : ScriptableObject
{
    public GameObject Prefab;
    public ItemObjectType Type;

    [TextArea(15,20)]
    public string Description;
}
