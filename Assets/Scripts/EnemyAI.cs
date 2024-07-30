using Pathfinding;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    AIPath aiPath;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        aiPath.canMove = false;
    }

    public void ActivateMovement()
    {
        aiPath.canMove = true;
        Debug.Log("Attack");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player") return;

        Debug.Log("Game end");
    }
}
