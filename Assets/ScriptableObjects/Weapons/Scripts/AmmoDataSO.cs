using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    hitscan,
    projectile
}

public class AmmoDataSO : MonoBehaviour
{
    public string Name;

    public string Description;
    public GameObject BulletPrefab;

    public ShootType AmmoType;
    
    public float Damage;
    public float HeadshotMultiplier;
    
    public float FireRate;
    public float Accuracy;

    public float Range;
    public float Speed;
    
    public float DropOff;
    public float Size;
    
    public bool Explosive;
    public float ExplosionRadius;
}
