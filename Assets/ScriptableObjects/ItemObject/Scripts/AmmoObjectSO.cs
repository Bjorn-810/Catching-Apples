using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo Object", menuName = "ScriptableObjects/Items/AmmoObject", order = 1)]
public class AmmoObjectSO : AbsctractItemObjectSO
{
    public AmmoDataSO AmmoData;

    public void Awake()
    {
        Type = ItemObjectType.Ammo;
    }
}
