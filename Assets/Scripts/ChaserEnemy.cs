using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{
    public float normalSpeed = 3f;
    public float chaseSpeed = 6f;
    public float chaseDistance = 4f;

    private Transform player;

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    private void Update()
    {
        if (GameManager.Instance != null)
        {
            if (!GameManager.Instance.gameHasStarted) return;
            if (GameManager.Instance.gameIsOver) return;
        }

        MoveChaser();
    }

    private void MoveChaser()
    {
        float speed = normalSpeed;

        if (player != null)
        {
            float distance = transform.position.x - player.position.x;

            // If close to player → speed up (chase)
            if (distance < chaseDistance)
            {
                speed = chaseSpeed;
            }
        }

        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}