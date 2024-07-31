using Pathfinding;
using System.Collections;
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
    }

    public IEnumerator StunMovement()
    {
        aiPath.canMove = false;

        yield return new WaitForSeconds(5);

        ActivateMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag != "Player") return;

        Debug.Log("Game end");
    }
}
