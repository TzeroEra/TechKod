using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Zenject;

public class AbilityManager
{
    private IAbility activeAbility;
    private IEnergyProvider energyProvider;
    private int shieldCount = 1;
    //public event Action<int> onShieldCountChanged;

    public AbilityManager (IEnergyProvider energyProvider)
    {
        this.energyProvider = energyProvider;
    }

    public void SetShieldCount(int count)
    {
        shieldCount = count; 
    }

    public int GetShieldCount() 
    {
        return shieldCount;
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