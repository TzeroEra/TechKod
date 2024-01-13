using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Shotgun : MonoBehaviour, IWeapon
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    public int numberOfBullets = 3;
    public float spreadAngle = 10f;

    public void Shoot(Vector3 targetPosition)
    {
        Vector2 shootDirection = (targetPosition - firePoint.position).normalized;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float randomSpread = Random.Range(-spreadAngle, spreadAngle);
            Vector2 spread = Quaternion.Euler(0, 0, randomSpread) * shootDirection;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = spread * bulletForce;
            Destroy(bullet, bullet.GetComponent<Bullet>().lifeTime);
        }
    }
}