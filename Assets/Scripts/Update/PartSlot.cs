using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartSlot : MonoBehaviour
{
    private Part part;
    private PartShop partShop;
    private int slotIndex;

    public void Setup(Part part, PartShop partShop, int slotIndex)
    {
        this.part = part;
        this.partShop = partShop;
        this.slotIndex = slotIndex;
    }

    public void UsePart()
    {
        Debug.Log("Застосовано бонус: " + part.name);

        if (part.bonusType == Part.BonusType.Health)
        {

        }

        Destroy(gameObject);
        partShop.UpdateSlots();
    }
}