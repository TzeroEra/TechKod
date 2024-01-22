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
        var player = other.GetComponent<PlayerMovement>();
        if (player != null && gameObject.CompareTag("EnemyBullet"))
        {
            player.TakeDamage(damage);
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("EnemyBullet") && !other.CompareTag("TurelEnemy"))
        {
            Destroy(gameObject);
        }

        //if (other.CompareTag("Player"))
        //{
        //    Bullet bullet = other.GetComponent<Bullet>();
        //    if (bullet != null)
        //    {
        //        TakeDamage(bullet.damage);
        //        Destroy(other.gameObject);
        //    }
        //}
    }
}