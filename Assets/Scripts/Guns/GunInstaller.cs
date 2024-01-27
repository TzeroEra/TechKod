using UnityEngine;
using Zenject;

public class GunInstaller : MonoInstaller
{
    public GameObject[] weaponPrefabs;

    public override void InstallBindings()
    {
        Container.Bind<WeaponFactory>().ToSelf().AsSingle();

        //foreach (var weaponPrefab in weaponPrefabs)
        //{
        //    Container.Bind<IWeapon>().FromComponentInNewPrefab(weaponPrefab).AsCached();
        //    Debug.Log($"Bound IWeapon to prefab: {weaponPrefab.name}");
        //}
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
                Debug.Log($"Loading weapon: {typeEnum}");
                var weaponInstance = _diContainer.InstantiatePrefabForComponent<IWeapon>(weaponPrefab);
                Debug.Log($"Weapon loaded successfully: {typeEnum}");
                return weaponInstance;
            }
        }

        Debug.LogError($"Failed to load weapon: {typeEnum}. Prefab not found.");
        return null;
    }
}