using UnityEngine;

public interface IEnemySpawner
{
    void SpawnEnemy(Vector3 spawnPoint, EnemyParameters parameters);
}