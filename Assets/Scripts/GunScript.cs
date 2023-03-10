using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [SerializeField]
    private Camera _cam;

    [SerializeField]
    private AmmoDataSO _ammoData;

    [SerializeField]
    private Transform _targetTransform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (_ammoData.AmmoType == ShootType.projectile)
            {
                ProjectileShoot(_ammoData.Damage, _ammoData.Speed, _ammoData.Range, _ammoData.DropOff);
                Debug.Log("ShootProjectile");
            }

            else if (_ammoData.AmmoType == ShootType.hitscan)
            {
                HitScanShoot(_ammoData.Damage, _ammoData.Range);
                Debug.Log("ShootHitscan");
            }
        }

        _targetTransform.position = GetReticleHit();
    }

    private Vector3 GetReticleHit()
    {
        RaycastHit hitInfo;
        Ray ray = _cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        Physics.Raycast(ray, out hitInfo);
        return hitInfo.point;
    }
    
    private void ProjectileShoot(float damageAmount, float speed, float range, float dropoff)
    {
        GameObject bullet = Instantiate(_ammoData.BulletPrefab, transform.position, Quaternion.identity);
        Vector3 shootDirection = (_targetTransform.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * _ammoData.Speed;

        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        
        bullet.GetComponent<ProjectileBullet>().carriedDamage = damageAmount;
        bullet.GetComponent<ProjectileBullet>().carriedSpeed = speed;
        bullet.GetComponent<ProjectileBullet>().carriedRange = range;
        bullet.GetComponent<ProjectileBullet>().carriedDamageDropOff = dropoff;
        bullet.GetComponent<ProjectileBullet>().carriedDamageDropOff = dropoff;
    }

    private void HitScanShoot(float damageAmount, float range)
    {
        RaycastHit hitInfo;

        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range))
        {
            Debug.Log("Hit " + hitInfo.transform.name + " at " + hitInfo.distance + " meters for " + damageAmount + " Damage");
        }
    }
}
