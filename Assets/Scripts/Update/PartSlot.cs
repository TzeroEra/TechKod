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

        // якщо це не викликаЇтьс€ автоматично, викличте метод дл€ отриманн€ залежност≥
        if (abilityManager == null)
        {
            abilityManager = GetComponent<AbilityManager>();
        }

        if (part != null)
        {
            ApplyBonusToPlayer();
        }
    }

    public void ApplyBonusToPlayer()
    {
        if (abilityManager != null && part != null && partShop != null)
        {
            Debug.Log($"Applying bonus to player: {part.bonusType}");

            if (part.bonusType == Part.BonusType.Shield)
            {
                // ќтримати поточну к≥льк≥сть щит≥в
                int currentShields = abilityManager.GetShieldCount();

                // «б≥льшити к≥льк≥сть щит≥в у два рази
                abilityManager.SetShieldCount(currentShields * 2);
            }
        }
        else
        {
            Debug.LogWarning("Part, PartShop, or AbilityManager is null");
        }
    }
}