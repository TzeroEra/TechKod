using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LaserGun : MonoBehaviour, IWeapon
{
    public Transform firePoint;
    public GameObject laserPrefab;
    public float laserForce = 30f;
    public float reloadTime = 3f;
    private float timeSinceLastShot = 0f;
    private bool canShoot = true;
    private GameObject currentLaser;
    private float laserGunReloadTime = 3f;
    public float ReloadTime { get => laserGunReloadTime; }

    public void Shoot(Vector3 targetPosition)
    {
        if (canShoot)
        {
            Vector2 shootDirection = (targetPosition - firePoint.position).normalized;
            currentLaser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = currentLaser.GetComponent<Rigidbody2D>();
            Destroy(currentLaser, laserPrefab.GetComponent<Laser>().lifeTime);

            canShoot = false;
            Invoke("ResetShoot", reloadTime);
        }
    }

    void Update()
    {
        if (currentLaser != null)
        {
            currentLaser.transform.position = firePoint.position;
        }
    }

    void ResetShoot()
    {
        canShoot = true;
        currentLaser = null;
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
}