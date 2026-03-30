using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("UI")]
    public Image healthBarFill;

    private CameraEffects cameraEffects;
    private SpriteRenderer skyRenderer;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        if (GameManager.Instance != null)
        {
            cameraEffects = GameManager.Instance.GetComponent<CameraEffects>();
        }

        GameObject sky = GameObject.Find("Sky");
        if (sky != null)
        {
            skyRenderer = sky.GetComponent<SpriteRenderer>();
        }
    }

    public void TakeDamage(int amount)
    {
        if (GameManager.Instance != null && GameManager.Instance.gameIsOver)
            return;

        currentHealth -= amount;

        if (currentHealth < 0)
            currentHealth = 0;

        UpdateHealthUI();

        if (cameraEffects != null)
        {
            cameraEffects.PlayDamageEffects();
        }

        if (currentHealth <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StopGame();
            }
        }
    }

    public void Heal(int amount)
    {
        if (GameManager.Instance != null && GameManager.Instance.gameIsOver)
            return;

        currentHealth += amount;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        float normalizedHealth = (float)currentHealth / maxHealth;

        if (healthBarFill != null)
        {
            healthBarFill.fillAmount = normalizedHealth;

            Color lowHealthColor = new Color(1f, 0.3f, 0.4f);   // soft red
            Color fullHealthColor = new Color(0.99f, 0.84f, 0.97f);

            healthBarFill.color = Color.Lerp(lowHealthColor, fullHealthColor, normalizedHealth);
        }

        if (skyRenderer != null)
        {
            Color dangerSky = new Color(1f, 0.7f, 0.75f);   // pink danger tone
            Color healthySky = new Color(0.75f, 0.9f, 1f); // light blue

            skyRenderer.color = Color.Lerp(dangerSky, healthySky, normalizedHealth);
        }
    }
}