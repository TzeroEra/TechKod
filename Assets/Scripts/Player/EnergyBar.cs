using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public int maxEnergy = 10;
    public Image[] energyBars; 

    private int currentEnergy; 

    void Start()
    {
        currentEnergy = maxEnergy; 
        UpdateEnergyBar();
    }

   
    void UpdateEnergyBar()
    {
        for (int i = 0; i < energyBars.Length; i++)
        {
            if (i < currentEnergy)
            {
                energyBars[i].enabled = true; 
            }
            else
            {
                energyBars[i].enabled = false; 
            }
        }
    }

    public void DecreaseEnergy(int amount)
    {
        currentEnergy -= amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy); 
        UpdateEnergyBar();
    }

    public void IncreaseEnergy(int amount)
    {
        currentEnergy += amount;
        currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        UpdateEnergyBar();
    }
}