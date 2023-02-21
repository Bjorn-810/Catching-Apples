using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ammunition Type", menuName = "Ammunition Type")]

public class AmmunitionType : ScriptableObject
{
    public string ammoName;
    public string ammoDescription;

    public bool isProjectile;

    public float damage;
    public float headshotMultiplier;
    
    public float speed;
    public float range;
    
    public float accuracy;
}
