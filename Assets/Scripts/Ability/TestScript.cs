using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;

public class TestScript : MonoBehaviour
{
    [Inject]
    private AbilityManager abilityManager;

    [SerializeField]
    private ShieldAbility shieldAbility;

    [SerializeField]
    private DestroyEnemiesAbility destroyEnemiesAbility;

    public TextMeshProUGUI energyText;

    private IAbility selectedAbility;

    public GameObject shieldButton;
    public GameObject destroyEnemiesButton;
    public GameObject Fon;

    public void Start()
    {
        destroyEnemiesButton.SetActive(true);
        shieldButton.SetActive(true);
        Fon.SetActive(true);
        Time.timeScale = 0f;
        UpdateEnergyText();
    }

    void Update()
    {
        if (selectedAbility != null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                ActivateSelectedAbility();
                Time.timeScale = 1f;
            }   
        }

        UpdateEnergyText();
    }

    private void ActivateSelectedAbility()
    {
        float energyCost = 1f;
        abilityManager.ActivateAbility(selectedAbility, energyCost);

        Fon.SetActive(false);
        if (shieldButton != null)
        {
            shieldButton.SetActive(false);
        }

        if (destroyEnemiesButton != null)
        {
            destroyEnemiesButton.SetActive(false);
        }
    }

    private void UpdateEnergyText()
    {
        int currentEnergy = Mathf.CeilToInt(abilityManager.EnergyProvider.CurrentEnergy);
        energyText.text = $"Ενεπγ³ÿ: {new string('|', currentEnergy)}";
    }

    public void SelectShieldAbility()
    {
        if (selectedAbility != shieldAbility)
        {
            selectedAbility = shieldAbility;
        }
    }

    public void SelectDestroyEnemiesAbility()
    {
        if (selectedAbility != destroyEnemiesAbility)
        {
            selectedAbility = destroyEnemiesAbility;
        }
    }
}