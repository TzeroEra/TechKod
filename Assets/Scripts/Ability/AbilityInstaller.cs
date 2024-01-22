using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityInstaller : MonoInstaller
{
    [SerializeField]
    private float initialEnergy = 10f;

    public override void InstallBindings()
    {
        Debug.Log($"Initial Energy: {initialEnergy}");
        Container.Bind<IEnergyProvider>().To<EnergyProvider>().AsSingle().WithArguments(initialEnergy);
        Container.Bind<AbilityManager>().AsSingle();
    }
}