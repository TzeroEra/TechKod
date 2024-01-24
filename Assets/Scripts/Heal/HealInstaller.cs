using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class HealInstaller : MonoInstaller
{
    [SerializeField] private GameObject healingZonePrefab;

    public override void InstallBindings()
    {
        Container.Bind<IHealingZoneFactory>().To<HealingZoneFactory>().AsTransient().WithArguments(healingZonePrefab);
    }
}