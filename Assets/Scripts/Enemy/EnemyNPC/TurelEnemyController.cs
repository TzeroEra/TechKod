using UnityEngine;

public class TurelEnemyController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float hoverHeight = 2f;
    public float shootingRange = 5f;

    private Transform player;
    private Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (player != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            Vector2 moveDirection = new Vector2(directionToPlayer.x, directionToPlayer.y).normalized;
            Vector2 targetPosition = (Vector2)player.position - moveDirection * shootingRange;
            MoveTowards(targetPosition);
            Hover();
        }
    }

    private void MoveTowards(Vector2 targetPosition)
    {
        Vector2 moveDirection = (targetPosition - rb.position).normalized;
        rb.velocity = moveDirection * moveSpeed;
    }

    private void Hover()
    {
        float newY = Mathf.Lerp(rb.position.y, hoverHeight, Time.deltaTime);
        rb.position = new Vector2(rb.position.x, newY);
    }
}