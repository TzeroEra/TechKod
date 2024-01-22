using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartButton : MonoBehaviour
{
    private Part part;
    private PartShop partShop;

    public void Setup(Part part, PartShop partShop)
    {
        this.part = part;
        this.partShop = partShop;
    }

    public void BuyPart()
    {
        if (partShop != null)
        {
            bool success = partShop.BuyPart(part);
        }
    }
}