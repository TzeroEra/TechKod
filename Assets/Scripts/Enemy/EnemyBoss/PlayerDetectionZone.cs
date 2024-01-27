using UnityEngine;

public interface IPlayerDetection
{
    bool IsPlayerDetected(Vector3 position);
}

public class PlayerDetectionZone : MonoBehaviour, IPlayerDetection
{
    public float detectionRadius = 10f;

    public bool IsPlayerDetected(Vector3 position)
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRadius, LayerMask.GetMask("Player"));
        bool isDetected = playerCollider != null;

        if (isDetected)
        {
            Debug.Log("Player detected in detection zone!");
        }
        else
        {
            Debug.Log("Player not detected in detection zone.");
        }

        return isDetected;
    }
}