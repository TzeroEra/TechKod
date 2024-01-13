using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public GameObject playerPrefab;
    public GameObject mouseControllerPrefab;

    public override void InstallBindings()
    {
        Container.Bind<Rigidbody2D>().FromComponentInNewPrefab(playerPrefab).AsSingle();
        Container.Bind<PlayerMovement>().AsSingle();
    }
}