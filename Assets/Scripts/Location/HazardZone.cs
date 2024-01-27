using UnityEngine;

public class HazardZone : MonoBehaviour
{
    public float damagePerSecond = 10f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }
}