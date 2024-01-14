using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRadius = 5f;

    private bool isChasingPlayer = false;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void Update()
    {
        if (IsPlayerWithinDetectionRadius())
        {
            isChasingPlayer = true;
        }

        if (isChasingPlayer)
        {
            MoveTowardsPlayer();
        }
    }

    private bool IsPlayerWithinDetectionRadius()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            return distanceToPlayer <= detectionRadius;
        }

        return false;
    }

    private void MoveTowardsPlayer()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}