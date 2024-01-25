using UnityEngine;
using Zenject;

public class GunInstaller : MonoInstaller
{
    public GameObject shotgunPrefab;
    public GameObject longRangeGunPrefab;
    public UIManagerScore uiManagerScorePrefab;

    public override void InstallBindings()
    {
		Container.Bind<WeaponFactory>().ToSelf().AsSingle();

		Container.Bind<Shotgun>().FromComponentInNewPrefab(shotgunPrefab).AsSingle();
        Container.Bind<LongRangeGun>().FromComponentInNewPrefab(longRangeGunPrefab).AsSingle();
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
        var all = Resources.LoadAll("Prefabs/Weapons");
        foreach (var item in all)
        {
            if (item.name == typeEnum)
            {
                // Instantinate...
            }
        }

        var weapon = Resources.Load<GameObject>($"Prefabs/Weapons/{typeEnum}");

        return _diContainer.InstantiatePrefabForComponent<IWeapon>(weapon);
    }
}