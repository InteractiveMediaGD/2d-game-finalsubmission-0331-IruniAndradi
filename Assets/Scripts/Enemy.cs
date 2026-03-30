using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Vertical Movement")]
    public float floatSpeed = 2f;
    public float floatHeight = 1f;

    [Header("Combat")]
    public int damageToPlayer = 20;
    public int scoreOnDestroy = 2;

    private Vector3 startPosition;
    private bool isDestroyed = false;

    private void Start()
    {
        startPosition = transform.position;

        floatSpeed = Random.Range(1.5f, 3f);
        floatHeight = Random.Range(0.5f, 1.5f);
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.gameIsOver)
            return;

        MoveUpDown();
    }

    private void MoveUpDown()
    {
        float newY = startPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance != null && GameManager.Instance.gameIsOver)
            return;

        if (isDestroyed) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageToPlayer);
            }

            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    public void DestroyByProjectile()
    {
        if (isDestroyed) return;

        isDestroyed = true;

        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreOnDestroy);
        }

        Destroy(gameObject);
    }
}