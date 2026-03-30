using UnityEngine;

public class HealthPackSpawner : MonoBehaviour
{
    public GameObject healthPackPrefab;
    public float spawnInterval = 5f;
    public float minY = -3.5f;
    public float maxY = 3.5f;

    private float timer = 0f;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.gameHasStarted) return;
            if (GameManager.Instance.gameIsOver) return;
        }

        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnHealthPack();
            timer = 0f;
        }
    }

    private void SpawnHealthPack()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);

        Instantiate(healthPackPrefab, spawnPosition, Quaternion.identity);
    }
}