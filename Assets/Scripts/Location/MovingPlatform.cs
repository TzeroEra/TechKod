using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2.0f;

    private Transform target;
    private bool isPlayerOnPlatform = false;
    private GameObject player;

    private void Start()
    {
        target = pointA;
    }

    private void Update()
    {
        MovePlatform();

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
            target = (target == pointA) ? pointB : pointA;
        }
    }

    private void MovePlayerWithPlatform()
    {
        if (player != null)
        {
            // «м≥нюЇмо батьк≥вський об'Їкт гравц€ на платформу
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