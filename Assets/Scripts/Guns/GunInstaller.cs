using UnityEngine;
using Zenject;

public class GunInstaller : MonoInstaller
{
    public GameObject[] weaponPrefabs;

    public override void InstallBindings()
    {
        Container.Bind<WeaponFactory>().ToSelf().AsSingle();
    }
}

public class WeaponFactory
{
    DiContainer _diContainer;

    public WeaponFactory(DiContainer container)
    {
        _diContainer = container;
    }

    public IWeapon Create(string typeEnum)
    {
        var allWeapons = Resources.LoadAll<GameObject>("Prefabs/Weapons");

        foreach (var weaponPrefab in allWeapons)
        {
            if (weaponPrefab.name == typeEnum)
            {
                var weaponInstance = _diContainer.InstantiatePrefabForComponent<IWeapon>(weaponPrefab);
                return weaponInstance;
            }
        }

        return null;
    }
}