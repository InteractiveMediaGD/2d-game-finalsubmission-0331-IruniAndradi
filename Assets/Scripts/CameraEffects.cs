using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraEffects : MonoBehaviour
{
    [Header("Damage Flash")]
    public Image damageFlashImage;
    public float flashDuration = 0.15f;
    public Color flashColor = new Color(1f, 0.4f, 0.5f, 0.35f);

    [Header("Camera Shake")]
    public Transform mainCamera;
    public float shakeDuration = 0.15f;
    public float shakeMagnitude = 0.15f;

    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Hit Sound")]
    public AudioClip hitSound;
    public float hitVolume = 0.8f;

    [Header("Collect Sound")]
    public AudioClip collectSound;
    public float collectVolume = 1f;

    [Header("Game Over Sound")]
    public AudioClip gameOverSound;
    public float gameOverVolume = 1f;

    private Vector3 originalCameraPosition;
    private Coroutine shakeCoroutine;

    private void Start()
    {
        if (mainCamera != null)
        {
            originalCameraPosition = mainCamera.localPosition;
        }

        if (damageFlashImage != null)
        {
            damageFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
        }
    }

    // 🔥 MAIN DAMAGE EFFECT (hit obstacle/enemy)
    public void PlayDamageEffects()
    {
        PlayDamageFlash();
        ShakeCamera();
        PlayHitSound();
    }

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound, hitVolume);
        }
    }

    // 💖 HEALTH PACK SOUND
    public void PlayCollectSound()
    {
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound, collectVolume);
        }
    }

    // 💀 GAME OVER SOUND
    public void PlayGameOverSound()
    {
        if (audioSource != null && gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound, gameOverVolume);
        }
    }

    // 💥 FLASH
    private void PlayDamageFlash()
    {
        if (damageFlashImage != null)
        {
            StopCoroutine("DamageFlashRoutine");
            StartCoroutine("DamageFlashRoutine");
        }
    }

    private IEnumerator DamageFlashRoutine()
    {
        damageFlashImage.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        damageFlashImage.color = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);
    }

    // 🎥 SHAKE
    private void ShakeCamera()
    {
        if (mainCamera == null) return;

        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            mainCamera.localPosition = originalCameraPosition;
        }

        shakeCoroutine = StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ShakeRoutine()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            mainCamera.localPosition = originalCameraPosition + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.localPosition = originalCameraPosition;
        shakeCoroutine = null;
    }
}