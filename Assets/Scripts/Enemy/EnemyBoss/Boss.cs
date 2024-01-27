using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth = 1000;
    private int currentHealth;

    public Transform[] movePoints;
    private int currentMovePointIndex;
    public float moveSpeed = 5f;
    public float distanceToChangeMovePoint = 0.1f;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 10f;

    public GameObject spikePrefab;
    public float spikeDuration = 2f;
    public float timeBetweenSpikes = 1f;

    public GameObject fallingColumnPrefab;
    public int numberOfColumns = 10;

    private bool isAttacking = false;

    private IPlayerDetection playerDetection;

    private float moveTimer;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();

        playerDetection = GetComponent<IPlayerDetection>();
        if (playerDetection == null)
        {
            Debug.LogError("PlayerDetectionZone component not found on Boss!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
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

    private void Update()
    {
        // Викликайте метод для руху до випадкової точки, якщо таймер досягнув нуля
        if (moveTimer <= 0f)
        {
            MoveToRandomPoint();

            // Скидайте таймер до вказаного періоду
            moveTimer = 10f;
        }
        else
        {
            // Зменште таймер
            moveTimer -= Time.deltaTime;
        }

        // Викликати метод атаки, якщо гравець виявлений
        if (playerDetection != null && playerDetection.IsPlayerDetected(transform.position))
        {
            if (!isAttacking)
            {
                StartCoroutine(PerformRandomAttack());
            }
        }
    }

    private void MoveToRandomPoint()
    {
        if (Vector2.Distance(transform.position, movePoints[currentMovePointIndex].position) > distanceToChangeMovePoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, movePoints[currentMovePointIndex].position, moveSpeed * Time.deltaTime);
        }
        else
        {
            currentMovePointIndex = Random.Range(0, movePoints.Length);
            Debug.Log("Moving to random point: " + movePoints[currentMovePointIndex].position);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
        else if (currentHealth % 50 != 0)
        {
            StartCoroutine(PerformRandomAttack());
        }
    }

    private void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }

    private IEnumerator PerformRandomAttack()
    {
        isAttacking = true;

        int randomAttack = Random.Range(1, 4);
        Debug.Log("Performing random attack: " + randomAttack);

        switch (randomAttack)
        {
            case 1:
                for (int i = 0; i < 3; i++)
                {
                    ShootInCircle();
                    yield return new WaitForSeconds(1f);
                }
                break;

            case 2:
                CallSpikes();
                yield return new WaitForSeconds(spikeDuration);
                break;

            case 3:
                DropColumns();
                yield return new WaitForSeconds(2f);
                break;
        }

        isAttacking = false;
    }

    private void ShootInCircle()
    {
        Debug.Log("Shooting in a circle");
        for (int i = 0; i < 360; i += 45)
        {
            Vector2 shootDirection = Quaternion.Euler(0, 0, i) * Vector2.up;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = shootDirection * bulletForce;
        }
    }

    private void CallSpikes()
    {
        Debug.Log("Calling spikes");
        StartCoroutine(SpawnSpikes());
    }

    private IEnumerator SpawnSpikes()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(spikePrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(timeBetweenSpikes);
        }
    }

    private void DropColumns()
    {
        Debug.Log("Dropping columns");
        for (int i = 0; i < numberOfColumns; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-5f, 5f), 10f);
            Instantiate(fallingColumnPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            float healthPercentage = (float)currentHealth / maxHealth;
            healthSlider.value = healthPercentage;
        }
    }
}