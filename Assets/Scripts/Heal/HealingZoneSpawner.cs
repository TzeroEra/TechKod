using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealingZoneSpawner : MonoBehaviour
{
    [Inject]
    private IHealingZoneFactory healingZoneFactory;

    public float spawnInterval = 2f;

    public Transform[] respawnPoints;

    public Transform[] nonRespawnPoints;

    private void Start()
    {
        SpawnHealingZones(nonRespawnPoints);
        StartCoroutine(CheckAndSpawn());
    }

    private IEnumerator CheckAndSpawn()
    {
        while (true)
        {
            foreach (Transform respawnPoint in respawnPoints)
            {
                if (respawnPoint.childCount == 0)
                {
                    Vector3 healingZonePosition = respawnPoint.position;
                    Transform newHealingZone = InstantiateHealingZone(healingZonePosition, respawnPoint);
                }
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Transform InstantiateHealingZone(Vector3 position, Transform parent)
    {
        Transform newHealingZone = healingZoneFactory.CreateHealingZone(position).transform;
        newHealingZone.parent = parent;
        return newHealingZone;
    }

    private void SpawnHealingZones(Transform[] points)
    {
        foreach (Transform point in points)
        {
            Vector3 healingZonePosition = point.position;
            InstantiateHealingZone(healingZonePosition, point);
        }
    }
}