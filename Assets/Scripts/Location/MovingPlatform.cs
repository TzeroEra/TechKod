using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;
    public float stopTime = 2.0f; 
    private float timer = 0.0f; 
    private bool isPlatformStopped = false;

    private Transform target;
    private bool isPlayerOnPlatform = false;
    private GameObject player;

    private void Start()
    {
        target = pointA;
    }

    private void Update()
    {
        if (!isPlatformStopped)
        {
            MovePlatform();
        }
        else
        {
            timer += Time.deltaTime;

            if (timer >= stopTime)
            {
                isPlatformStopped = false;
                timer = 0.0f;
            }
        }

        if (isPlayerOnPlatform)
        {
            MovePlayerWithPlatform();
        }
    }

    private void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            isPlatformStopped = true;
            target = (target == pointA) ? pointB : pointA;
        }
    }

    private void MovePlayerWithPlatform()
    {
        if (player != null)
        {
            player.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            player = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            player.transform.SetParent(null, true);
            player = null;
        }
    }
}