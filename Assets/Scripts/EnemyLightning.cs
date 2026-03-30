using UnityEngine;
using System.Collections;

public class EnemyLightning : MonoBehaviour
{
    public SpriteRenderer lightningRenderer;
    public float minBlinkTime = 0.15f;
    public float maxBlinkTime = 0.5f;

    private void Start()
    {
        if (lightningRenderer == null)
        {
            lightningRenderer = GetComponent<SpriteRenderer>();
        }

        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            lightningRenderer.enabled = true;
            yield return new WaitForSeconds(Random.Range(minBlinkTime, maxBlinkTime));

            lightningRenderer.enabled = false;
            yield return new WaitForSeconds(Random.Range(minBlinkTime, maxBlinkTime));
        }
    }
}