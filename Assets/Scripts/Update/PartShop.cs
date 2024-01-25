using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartShop : MonoBehaviour
{
    public GameObject partCanvas;
    public GameObject partCanvas_2;
    public List<Part> availableParts;
    public List<Transform> slots;
    public int maxSlots = 2;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = ScoreManager.Instance;

        if (scoreManager != null)
        {
            //scoreManager.SetScoreUI(new UIManagerScore());
            UpdateSlots();
        }

        partCanvas.SetActive(false);
        partCanvas_2.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            TogglePartCanvas();
        }
    }

    private void TogglePartCanvas()
    {
        partCanvas.SetActive(!partCanvas.activeSelf);
        partCanvas_2.SetActive(!partCanvas_2.activeSelf);
        Time.timeScale = partCanvas.activeSelf ? 0f : 1f;
    }

    public void UpdateSlots()
    {
        foreach (Transform slot in slots)
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        int slotIndex = 0;
        foreach (Part part in availableParts)
        {
            if (slotIndex >= maxSlots)
            {
                break;
            }

            if (part.isPurchased)
            {
                GameObject partObject = Instantiate(part.prefab, slots[slotIndex]);
                PartButton partButton = partObject.GetComponent<PartButton>();
                partButton.Setup(part, this);

                slotIndex++;
            }
        }
    }

    public bool BuyPart(Part part)
    {
        if (scoreManager != null && scoreManager.Score >= part.cost && !part.isPurchased)
        {
            scoreManager.AddScore(-part.cost);
            part.isPurchased = true;

            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].childCount == 0)
                {
                    GameObject partObject = new GameObject("PartSlot");
                    PartSlot partSlotComponent = partObject.AddComponent<PartSlot>();

                    partSlotComponent.Setup(part, this, i);
                    partObject.transform.parent = slots[i];

                    return true;
                }
            }
        }

        return false;
    }


    public void ApplyBonusToPlayer(Part.BonusType bonusType)
    {
       
    }

    public void OnAvailablePartClick(int partIndex)
    {
        if (partIndex >= 0 && partIndex < availableParts.Count)
        {
            Part selectedPart = availableParts[partIndex];

            if (scoreManager != null && scoreManager.Score >= selectedPart.cost)
            {
                BuyPart(selectedPart);
            }
            else
            {
                Debug.Log("Недостатньо очок для купівлі запчастини: " + selectedPart.name);
            }
        }
    }
}

[System.Serializable]
public class Part
{
    public string name;
    public GameObject prefab;
    public int cost;
    public bool isPurchased = false;
    public BonusType bonusType;

    public enum BonusType
    {
        None, 
        Health, 
        Damage 
    }
}