using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LongRangeGun : MonoBehaviour, IWeapon
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;

    public void Shoot(Vector3 targetPosition)
    {
        Vector2 shootDirection = (targetPosition - firePoint.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = shootDirection * bulletForce;
        Destroy(bullet, bullet.GetComponent<Bullet>().lifeTime);
    }
}