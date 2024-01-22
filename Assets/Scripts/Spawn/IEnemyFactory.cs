using UnityEngine;

public interface IEnemyFactory
{
    GameObject CreateEnemy(Vector3 position, EnemySettings settings);
    //string Tag
}

