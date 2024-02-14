using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
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

    public GameObject fallingColumnPrefab;
    public int numberOfColumns = 10;

    public Tilemap tilemapGrid1;  // Посилання на перший Tilemap
    public Tilemap tilemapGrid2;  // Посилання на другий Tilemap
    private bool isGrid1Active = true;
    private bool isGrid2Active = true;

    private bool isAttacking = false;

    private float moveTimer;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
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
        if (moveTimer <= 0f)
        {
            MoveToRandomPoint();
            moveTimer = 10f;
        }
        else
        {
            moveTimer -= Time.deltaTime;
        }

        if (!isAttacking)
        {
            StartCoroutine(PerformRandomAttack());
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
    }

    private void Die()
    {
        Debug.Log("Boss defeated!");
        Destroy(gameObject);
    }

    private IEnumerator PerformRandomAttack()
    {
        isAttacking = true;

        int randomAttack = Random.Range(1, 3);
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
                ToggleGridForDuration(tilemapGrid1, ref isGrid1Active, 5f);
                yield return new WaitForSeconds(5f);
                break;

            case 3:
                ToggleGridForDuration(tilemapGrid2, ref isGrid2Active, 5f);
                yield return new WaitForSeconds(5f);
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

    private void ToggleGridForDuration(Tilemap tilemap, ref bool isGridActive, float duration)
    {
        Debug.Log("Toggling Grid for " + duration + " seconds");

        // Змінюємо стан Grid
        isGridActive = !isGridActive;
        tilemap.gameObject.SetActive(isGridActive);
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