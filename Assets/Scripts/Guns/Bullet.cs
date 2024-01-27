using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2.0f;
    public int damage = 10; 

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("EnemyBullet"))
        {
            if (other.CompareTag("Player"))
            {
                PlayerMovement player = other.GetComponent<PlayerMovement>();
                if (player != null)
                {
                    player.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
            else if (!other.CompareTag("EnemyBullet") && !other.CompareTag("TurelEnemy") && !other.CompareTag("StaticEnemy") && !other.CompareTag("MovingEnemy"))
            {
                Destroy(gameObject);
            }
        }
    }
}