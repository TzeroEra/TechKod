using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    public EnemySettingsSO enemySettings;

    [Inject]
    public IEnemyFactory enemyFactory;

    public int numberOfEnemiesToSpawn = 5;
    public float spawnInterval = 2f;
    public bool isRepeatingSpawn = true;

    public SpawnType spawnType = SpawnType.RandomNearPoint;

    public enum SpawnType
    {
        RandomNearPoint,
        DirectlyOnPoint
    }

    private void Start()
    {
        if (isRepeatingSpawn)
        {
            InvokeRepeating("SpawnEnemy", 0f, spawnInterval);
        }
        else
        {
            SpawnEnemiesOnce();
        }
    }

    void SpawnEnemy()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 spawnPosition;

            switch (spawnType)
            {
                case SpawnType.RandomNearPoint:
                    spawnPosition = transform.position + Random.insideUnitSphere * 5f;
                    break;

                case SpawnType.DirectlyOnPoint:
                    spawnPosition = transform.position;
                    break;

                default:
                    spawnPosition = transform.position;
                    break;
            }
            enemyFactory.CreateEnemy(spawnPosition, enemySettings);
        }
    }

    void SpawnEnemiesOnce()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            Vector3 spawnPosition;

            switch (spawnType)
            {
                case SpawnType.RandomNearPoint:
                    spawnPosition = transform.position + Random.insideUnitSphere * 5f;
                    break;

                case SpawnType.DirectlyOnPoint:
                    spawnPosition = transform.position;
                    break;

                default:
                    spawnPosition = transform.position;
                    break;
            }

            enemyFactory.CreateEnemy(spawnPosition, enemySettings);
        }
    }
}