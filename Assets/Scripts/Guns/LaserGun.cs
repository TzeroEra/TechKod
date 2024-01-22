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
    private GameObject currentLaser; // Зберігає посилання на поточний лазер

    public void Shoot(Vector3 targetPosition)
    {
        if (canShoot)
        {
            Vector2 shootDirection = (targetPosition - firePoint.position).normalized;
            currentLaser = Instantiate(laserPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = currentLaser.GetComponent<Rigidbody2D>();
            // Не встановлюємо швидкість, оскільки ми будемо змінювати позицію вручну
            Destroy(currentLaser, laserPrefab.GetComponent<Laser>().lifeTime);

            canShoot = false;
            Invoke("ResetShoot", reloadTime);
        }
    }

    void Update()
    {
        // Перевіряємо, чи є поточний лазер і змінюємо його позицію відповідно до позиції гравця
        if (currentLaser != null)
        {
            currentLaser.transform.position = firePoint.position;
        }
    }

    void ResetShoot()
    {
        canShoot = true;
        // Знімемо можливість стріляти тільки після того, як лазер буде знищений
        currentLaser = null;
    }
}