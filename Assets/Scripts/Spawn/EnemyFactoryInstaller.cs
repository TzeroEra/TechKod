using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactoryInstaller : MonoInstaller
{
    [SerializeField] private GameObject enemyPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsTransient().WithArguments(enemyPrefab);
    }
}