using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float destroyX = -15f;

    private void Update()
    {
        if (GameManager.Instance == null) return;
        if (!GameManager.Instance.gameHasStarted) return;
        if (GameManager.Instance.gameIsOver) return;

        transform.Translate(Vector3.left * GameManager.Instance.worldSpeed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}