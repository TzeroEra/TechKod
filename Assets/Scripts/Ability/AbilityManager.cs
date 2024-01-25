using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AbilityManager : MonoBehaviour
{
    private IAbility activeAbility;
    private IEnergyProvider energyProvider;

    [Inject]
    public void Construct(IEnergyProvider energyProvider)
    {
        this.energyProvider = energyProvider;
    }

    public void AddShield()
    {
		if (activeAbility is IAddToAbilitySlot shield)
        {
			shield.AddShield();
        }
    }

    public IEnergyProvider EnergyProvider => energyProvider;

    public void ActivateAbility(IAbility ability, float energyCost)
    {
        if (energyProvider.CanConsumeEnergy(energyCost))
        {
            energyProvider.ConsumeEnergy(energyCost);
            activeAbility = ability;
            activeAbility.Activate();
        }
        else
        {
            Debug.Log("Not enough energy to activate the ability!");
        }
    }
}