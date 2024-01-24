using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Масив точок для спавну гравця
    public GameObject playerPrefab;  // Префаб гравця

    private void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (spawnPoints.Length == 0 || playerPrefab == null)
        {
            Debug.LogError("Please assign spawn points and player prefab in the inspector.");
            return;
        }

        // Вибираємо випадкову точку для спавну гравця
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Спавнимо гравця на вибраній точці
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // Додаткові налаштування гравця (якщо необхідно)
        // player.GetComponent<YourPlayerScript>().SetupPlayer();

        Debug.Log("Player spawned at: " + spawnPoint.position);
    }
}