using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DestroyEnemiesAbility : MonoBehaviour, IAbility
{
    public GameObject destructionCirclePrefab;
    public float destructionCircleDuration = 3f;

    public void Activate()
    {
        GameObject destructionCircle = Instantiate(destructionCirclePrefab, transform.position, Quaternion.identity);
        StartCoroutine(EnableAndDisableCircleAfterDuration(destructionCircle));
    }

    private IEnumerator EnableAndDisableCircleAfterDuration(GameObject destructionCircle)
    {
        destructionCircle.SetActive(true);
        yield return new WaitForSeconds(destructionCircleDuration);
        destructionCircle.SetActive(false);
    }
}