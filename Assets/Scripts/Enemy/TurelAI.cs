using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public enum EnemyType
{
    Moving,
    Static,
    Turel
}

public enum WeaponType
{
    Normal,
    Shotgun,
    Sniper,
    Minigun
}

public class TurelAI : MonoBehaviour, IScoreManager
{
    public GameObject bulletPrefab;
    private float bulletSpeed;
    private float shootingInterval;
    private float shootingRange;
    private int shotgunPelletCount;
    private float minigunShootingInterval;
    private int minigunBulletCount;
    private float minigunSpreadAngle;
    private bool canShoot = true;
    private Transform player;

    public Slider healthBar;
    public Canvas canvas;

    private float maxHealth;
    private float currentHealth;
    private int pointsForDestruction;
    private EnemyType enemyType;
    private WeaponType weaponType;

    private MovingEnemyController movingEnemyController;
    private TurelEnemyController turelEnemyController;
    public EnemySettingsSO enemySettings;

    [Inject]
    private IScoreManager scoreManager;

    void Start()
    {
        if (enemySettings == null)
        {
            Debug.LogError("EnemySettings component not found! Attach it via Unity Inspector.");
            return;
        }

        shootingRange = enemySettings.shootingRange;
        maxHealth = enemySettings.maxHealth;
        enemyType = enemySettings.enemyType;
        weaponType = enemySettings.weaponType;
        bulletSpeed = enemySettings.bulletSpeed;
        shootingInterval = enemySettings.shootingInterval;
        shotgunPelletCount = enemySettings.shotgunPelletCount;
        minigunShootingInterval = enemySettings.minigunShootingInterval;
        minigunBulletCount = enemySettings.minigunBulletCount;
        minigunSpreadAngle = enemySettings.minigunSpreadAngle;
        pointsForDestruction = enemySettings.pointsForDestruction;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentHealth = maxHealth;
        UpdateHealthBar();

        switch (enemyType)
        {
            case EnemyType.Moving:
                gameObject.tag = "MovingEnemy";
                movingEnemyController = gameObject.AddComponent<MovingEnemyController>();
                break;

            case EnemyType.Static:
                gameObject.tag = "StaticEnemy";
                break;

            case EnemyType.Turel:
                gameObject.tag = "TurelEnemy";
                turelEnemyController = gameObject.AddComponent<TurelEnemyController>();
                Rigidbody2D rbMoving = gameObject.AddComponent<Rigidbody2D>();
                rbMoving.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;

            default:
                Debug.LogError("Unknown enemy type");
                break;
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

                    float interval = (weaponType == WeaponType.Minigun) ? minigunShootingInterval : shootingInterval;

                    Invoke("ResetShoot", interval);

                    switch (weaponType)
                    {
                        case WeaponType.Shotgun:
                            for (int i = 0; i < shotgunPelletCount; i++)
                            {
                                ShootWithSpread(i * 10f);
                            }
                            break;

                        case WeaponType.Sniper:
                            ShootStraight();
                            break;

                        case WeaponType.Minigun:
                            for (int i = 0; i < minigunBulletCount; i++)
                            {
                                ShootStraight();
                            }
                            break;

                        default:
                            ShootStraight();
                            break;
                    }
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

    public void Die()
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(pointsForDestruction);
            Debug.Log("Points after destruction: " + pointsForDestruction);
        }
        else
        {
            Debug.LogError("ScoreManager is not assigned. Make sure to assign it in the Inspector or through dependency injection.");
        }

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

    void ShootStraight()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = direction * bulletSpeed;
        Destroy(bullet, 2f);
    }

    void ShootWithSpread(float angleOffset)
    {
        Vector3 direction = Quaternion.Euler(0f, 0f, angleOffset) * (player.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = direction * bulletSpeed;
        Destroy(bullet, 2f);
    }

    public void AddScore(int points)
    {
        if (scoreManager != null)
        {
            scoreManager.AddScore(points);
        }
        else
        {
            Debug.LogError("ScoreManager is not assigned. Make sure to assign it in the Inspector or through dependency injection.");
        }
    }
}

