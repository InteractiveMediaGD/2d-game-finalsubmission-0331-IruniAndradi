using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float normalSpeed = 5f;
    public float boostedSpeed = 7f;
    public float minY = -4f;
    public float maxY = 4f;

    [Header("Shooting")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float shootCooldown = 0.25f;

    [Header("Shooting Audio")]
    public AudioClip shootSound;
    public float shootVolume = 0.8f;

    private float shootTimer = 0f;

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.gameHasStarted) return;
            if (GameManager.Instance.gameIsOver) return;
            if (GameManager.Instance.isPaused) return;
        }

        MovePlayer();

        shootTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && shootTimer >= shootCooldown)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void MovePlayer()
    {
        float vertical = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            vertical = 1f;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            vertical = -1f;
        }

        float currentSpeed = normalSpeed;

        if (GameManager.Instance != null && GameManager.Instance.isPhaseTwo)
        {
            currentSpeed = boostedSpeed;
        }

        Vector3 movement = new Vector3(0f, vertical, 0f) * currentSpeed * Time.deltaTime;
        transform.position += movement;

        float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
    }

    private void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            PlayShootSound();
        }
    }

    private void PlayShootSound()
    {
        if (GameManager.Instance != null)
        {
            CameraEffects effects = GameManager.Instance.GetComponent<CameraEffects>();

            if (effects != null && effects.audioSource != null && shootSound != null)
            {
                effects.audioSource.PlayOneShot(shootSound, shootVolume);
            }
        }
    }
}