using System.Collections;
using UnityEngine;

public class ShieldAbility : MonoBehaviour, IAbility
{
    public GameObject rotatingPart;
    public GameObject protectivePart;
    public float rotationSpeed = 50f;
    public float shieldDuration = 10f;

    public void Activate()
    {
        CreateShield();
        StartCoroutine(RotateShield());
        StartCoroutine(ShieldTimer());
    }

    private void CreateShield()
    {
        rotatingPart.SetActive(true);
        protectivePart.SetActive(true);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            Debug.Log("Пошкодження отримано, але щит захистив вас!");
        }
    }
}