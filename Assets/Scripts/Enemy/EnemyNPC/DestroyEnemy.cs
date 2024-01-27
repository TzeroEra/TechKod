using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("StaticEnemy") || other.CompareTag("TurelEnemy"))
        {
            DestroyTarget(other.gameObject);
        }
    }

    private void DestroyTarget(GameObject target)
    {
        var enemyScript = target.GetComponent<TurelAI>();
        if (enemyScript != null)
        {
            enemyScript.Die();
        }
    }
}