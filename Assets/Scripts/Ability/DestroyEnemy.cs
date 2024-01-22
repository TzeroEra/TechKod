using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemy : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D called");
        if (other.CompareTag("StaticEnemy") || other.CompareTag("TurelEnemy"))
        {
            DestroyTarget(other.gameObject);
        }
    }

    private void DestroyTarget(GameObject target)
    {
        Debug.Log("�������� ������: " + target.name);

        var enemyScript = target.GetComponent<TurelAI>();
        if (enemyScript != null)
        {
            Debug.Log("������ ������ ��������. ������ ������ Die().");
            enemyScript.Die();
        }
        else
        {
            Debug.LogError("������ ������ �� �������� �� ��'��� � �����: " + target.tag);
        }
    }
}