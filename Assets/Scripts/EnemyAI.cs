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
        Debug.Log("move");
        aiPath.canMove = true;
    }

    public IEnumerator StunMovement()
    {
        Debug.Log("stuck");
        aiPath.canMove = false;

        yield return new WaitForSeconds(5);

        ActivateMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("flash"))
        {
            StartCoroutine(StunMovement());
        }

        if (!collision.gameObject.CompareTag("Player")) return;

        Debug.Log("Game end");
    }
}
