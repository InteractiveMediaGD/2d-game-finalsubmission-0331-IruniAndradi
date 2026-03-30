using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool alreadyScored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyScored) return;

        if (other.CompareTag("Player"))
        {
            alreadyScored = true;
            GameManager.Instance.AddScore(1);
        }
    }
}