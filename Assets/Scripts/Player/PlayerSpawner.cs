using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // ����� ����� ��� ������ ������
    public GameObject playerPrefab;  // ������ ������

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

        // �������� ��������� ����� ��� ������ ������
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // �������� ������ �� ������� �����
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        // �������� ������������ ������ (���� ���������)
        // player.GetComponent<YourPlayerScript>().SetupPlayer();

        Debug.Log("Player spawned at: " + spawnPoint.position);
    }
}