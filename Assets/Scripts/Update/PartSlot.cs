using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class PartSlot : MonoBehaviour
{
    private Part part;
    private PartShop partShop;
    private int slotIndex;

    private AbilityManager abilityManager;

    [Inject]
    private void Construct (AbilityManager abilityManager)
    {
        this.abilityManager = abilityManager;

        Debug.Log("Loger" + (this.abilityManager == null).ToString());

    }

    public void Setup(Part part, PartShop partShop, int slotIndex)
    {
        this.part = part;
        this.partShop = partShop;
        this.slotIndex = slotIndex;

        if (part != null)
        {
            ApplyBonusToPlayer();
        }
    }

    public void ApplyBonusToPlayer()
    {
        Debug.Log($"Applying bonus to player: {part.bonusType}");

        if (part.bonusType == Part.BonusType.Shield)
        {
            if (abilityManager == null)
            {
                Debug.LogError("Error");
            }

            int currentShields = abilityManager.GetShieldCount();
            abilityManager.SetShieldCount(currentShields * 2);
        }
    }
}