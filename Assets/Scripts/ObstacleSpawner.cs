using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePairPrefab;
    public float spawnInterval = 2.2f;
    public float minY = -1.5f;
    public float maxY = 1.5f;

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
            SpawnObstacle();
            timer = 0f;
        }
    }

    private void SpawnObstacle()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);

        Instantiate(obstaclePairPrefab, spawnPosition, Quaternion.identity);
    }
}