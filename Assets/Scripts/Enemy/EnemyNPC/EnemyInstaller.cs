using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("Installing bindings. IEnemyFactory to EnemyFactory");
        Container.Bind<IEnemyFactory>().To<EnemyFactory>().AsTransient();
    }
}

