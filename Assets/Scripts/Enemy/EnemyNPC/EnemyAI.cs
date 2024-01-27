using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float shootingInterval = 1f;
    public float shootingRange = 5f; 

    private bool canShoot = true;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}