using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyProvider : IEnergyProvider
{
    private float currentEnergy;

    public float CurrentEnergy => currentEnergy;

    public EnergyProvider(float initialEnergy)
    {
        currentEnergy = initialEnergy;
    }

    public bool CanConsumeEnergy(float amount)
    {
        return currentEnergy >= amount;
    }

    public void ConsumeEnergy(float amount)
    {
        if (CanConsumeEnergy(amount))
        {
            currentEnergy -= amount;
        }
    }
}