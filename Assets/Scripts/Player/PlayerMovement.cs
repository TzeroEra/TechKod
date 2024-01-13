using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private float groundCheckRadius = 0.2f;
    private bool isRolling = false;
    private bool isImmortal = false;
    public float immortalDuration = 2f;

    [SerializeField] private float moveInput;

    public Slider healthSlider; 
    private float maxHealth = 100f;
    private float currentHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        moveInput = Input.GetAxis("Horizontal");

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && rb.velocity.y <= 0.01f)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isRolling)
        {
            StartCoroutine(Roll());
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(EnableImmortality());
        }

        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    private IEnumerator Roll()
    {
        isRolling = true;
        float rollDistance = 5f;

        for (float t = 0; t < 1; t += Time.deltaTime / 0.5f)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            transform.position += transform.right * (moveInput * rollDistance * Time.deltaTime / 0.5f);
            yield return null;
        }

        isRolling = false;
    }

    private IEnumerator EnableImmortality()
    {
        isImmortal = true;
        yield return new WaitForSeconds(immortalDuration);
        isImmortal = false;
    }

    private void FixedUpdate()
    {
        if (!isRolling && !isImmortal)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            float healthPercentage = CalculateHealthPercentage();
            healthSlider.value = healthPercentage;
        }
    }

    private float CalculateHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        if (!isImmortal)
        {
            currentHealth -= damageAmount;
            currentHealth = Mathf.Max(currentHealth, 0f);
            UpdateHealthBar();

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if (bullet != null)
            {
                TakeDamage(bullet.damage);
                Destroy(other.gameObject);

                Debug.Log("Player took damage from enemy bullet!");
            }
        }
    }
}