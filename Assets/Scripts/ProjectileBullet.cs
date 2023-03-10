using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    public float carriedDamage;
    public float carriedHeadshotMultiplier;
    public float carriedDamageDropOff;

    public float carriedRange;
    
    public float carriedSpeed;
    public float carriedDropOff;

    public float carriedSize;

    public bool carriedExplosive;
    public float carriedExplosionRadius;
    
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
