using UnityEngine;
using Zenject;

public class GunInstaller : MonoInstaller
{
    public GameObject shotgunPrefab;
    public GameObject longRangeGunPrefab;
    public UIManagerScore uiManagerScorePrefab;

    public override void InstallBindings()
    {
        Container.Bind<Shotgun>().FromComponentInNewPrefab(shotgunPrefab).AsSingle();
        Container.Bind<LongRangeGun>().FromComponentInNewPrefab(longRangeGunPrefab).AsSingle();
    }
}