using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healAmount = 20;

    [Header("Audio")]
    public AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance != null && GameManager.Instance.gameIsOver)
            return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();

            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount);
            }

            PlayCollectSound();

            Destroy(gameObject);
        }
    }

    private void PlayCollectSound()
    {
        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position, 9f);
        }
    }
}