using UnityEngine;

public class HealingZone : MonoBehaviour
{
    public float healingAmount = 20f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Heal(healingAmount);
                Destroy(gameObject);
            }
        }
    }
}