using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private readonly GameObject _enemyPrefab;
    private readonly DiContainer _diContainer;

    public EnemyFactory(GameObject enemyPrefab, DiContainer diContainer)
    {
        _diContainer = diContainer;
        _enemyPrefab = enemyPrefab;
    }

    public GameObject CreateEnemy(Vector3 position, EnemySettings settings)
    //string Tag
    {

        GameObject newEnemy = _diContainer.InstantiatePrefab(_enemyPrefab);

        newEnemy.transform.position = position;

        TurelAI enemyAI = newEnemy.GetComponent<TurelAI>();

        if (enemyAI != null && settings != null)
        {
            enemyAI.enemySettings = settings;
        }

        return newEnemy;
    }
}

