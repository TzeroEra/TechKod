using UnityEngine;
using UnityEngine.UI;

public class PlayerDetectionZone : MonoBehaviour
{
    public float detectionRadius = 10f;
    public Boss boss;
    public Slider slider;

    private void Start()
    {
        if (slider != null)
        {
            slider.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered detection zone!");

            if (slider != null)
            {
                slider.gameObject.SetActive(true);
            }

            if (boss != null)
            {
                boss.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited detection zone!");

            if (slider != null)
            {
                slider.gameObject.SetActive(false);
            }

            if (boss != null)
            {
                boss.enabled = false;
            }
        }
    }
}