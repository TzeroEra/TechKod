using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    Moving,
    Static 
}

public enum WeaponType
{
    Normal,
    Shotgun,
    Sniper
}

public class TurelAI : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootingInterval = 1f;
    public float shootingRange = 5f;

    private bool canShoot = true;
    private Transform player;

    public Slider healthBar;
    public Canvas canvas;

    public float maxHealth = 100f;
    private float currentHealth;

    public int pointsForDestruction = 10;

    public EnemyType enemyType;
    public WeaponType weaponType;

    private MovingEnemyController movingEnemyController;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        UpdateHealthBar();

        if (enemyType == EnemyType.Moving)
        {
            movingEnemyController = gameObject.AddComponent<MovingEnemyController>();
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= shootingRange)
            {
                if (canShoot)
                {
                    canShoot = false;
                    Invoke("ResetShoot", shootingInterval);

                    Vector3 direction = (player.position - transform.position).normalized;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

                    GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
                    Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                    bulletRB.velocity = direction * bulletSpeed;
                    Destroy(bullet, 2f);
                }
            }
        }

        if (healthBar != null && canvas != null)
        {
            Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            healthBar.transform.position = screenPosition + new Vector2(0f, 30f);
        }

        if (healthBar != null)
        {
            float healthPercentage = CalculateHealthPercentage();
            healthBar.value = healthPercentage;
        }

        if (enemyType == EnemyType.Moving)
        {
            movingEnemyController.Update();
        }
    }

    void ResetShoot()
    {
        canShoot = true;
    }

    void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        currentHealth = Mathf.Max(currentHealth, 0f);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        ScoreManager.Instance.AddScore(pointsForDestruction);

        Destroy(gameObject);
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            float healthPercentage = CalculateHealthPercentage();
            healthBar.value = healthPercentage;
        }
    }

    float CalculateHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);
            }
        }
    }
}