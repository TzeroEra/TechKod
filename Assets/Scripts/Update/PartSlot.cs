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

    [Inject]
    private AbilityManager abilityManager;

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
        if (part != null && partShop != null)
        {
            Debug.Log($"Applying bonus to player: {part.bonusType}");
            partShop.ApplyBonusToPlayer(part.bonusType);
        }
        else
        {
            Debug.LogWarning("Part or PartShop is null");
        }
    }
}