using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LongRangeGun : MonoBehaviour, IWeapon
{
    private Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    private float longRangeGunReloadTime = 0.5f;
    public float ReloadTime { get => longRangeGunReloadTime; }

    public void Shoot(Vector3 targetPosition)
    {
        if (!CanShoot())
        {
            return;
        }

        Vector2 shootDirection = targetPosition - firePoint.position;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection.normalized * bulletForce;
        Destroy(bullet, bullet.GetComponent<Bullet>().lifeTime);

        ResetTimer();
    }

    private float LastTimeShot = 0;

    public bool CanShoot()
    {
        return LastTimeShot >= ReloadTime;
    }

    public void ResetTimer()
    {
        LastTimeShot = 0;
    }

    public void UpdateReloadTimers()
    {
        LastTimeShot += Time.deltaTime;
    }

    public void SetFirePoint(Transform tr)
    {
        firePoint = tr;
        transform.SetParent(tr);
    }
}