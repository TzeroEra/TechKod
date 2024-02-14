using System.Collections;
using UnityEngine;
using Zenject;

public interface IAddToAbilitySlot
{
    void AddShield();
}

public class ShieldAbility : MonoBehaviour, IAbility, IAddToAbilitySlot
{
    public GameObject rotatingPart;
    public GameObject protectivePart;
    public float rotationSpeed = 50f;
    public float shieldDuration = 10f;
    public GameObject protectivePart_2;

    [Inject]
    private AbilityManager abilityManager;

    public void Activate()
    {
        CreateShield();
        StartCoroutine(RotateShield());
        StartCoroutine(ShieldTimer());
    }

    public void AddShield()
    {
        Debug.Log("Final");
        if (abilityManager != null)
        {
            abilityManager.SetShieldCount(abilityManager.GetShieldCount() * 2);
        }
        protectivePart_2.SetActive(abilityManager.GetShieldCount() == 2);
    }

    private void CreateShield()
    {
        rotatingPart.SetActive(true);
        protectivePart.SetActive(true);

        protectivePart_2.SetActive(abilityManager.GetShieldCount() == 2);
    }

    private IEnumerator RotateShield()
    {
        while (true)
        {
            rotatingPart.transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(shieldDuration);
        DestroyShield();
    }

    private void DestroyShield()
    {
        rotatingPart.SetActive(false);

        if (protectivePart != null)
        {
            protectivePart.SetActive(false);
        }
    }
}