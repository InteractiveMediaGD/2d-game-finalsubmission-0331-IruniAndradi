using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject chaserEnemyPrefab;

    public float spawnInterval = 4f;
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

        float currentSpawnInterval = spawnInterval;

        if (GameManager.Instance != null && GameManager.Instance.isPhaseTwo)
        {
            currentSpawnInterval = spawnInterval * 0.6f;
        }

        if (timer >= currentSpawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, 0f);

        if (GameManager.Instance != null && GameManager.Instance.isPhaseTwo)
        {
            if (Random.value < 0.5f && chaserEnemyPrefab != null)
            {
                Instantiate(chaserEnemyPrefab, spawnPosition, Quaternion.identity);
            }
            else if (enemyPrefab != null)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            if (enemyPrefab != null)
            {
                Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}