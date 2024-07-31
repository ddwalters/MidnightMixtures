using Pathfinding;
using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    AIPath aiPath;
    AIDestinationSetter dest;
    GameObject Player;

    private void Start()
    {
        aiPath = GetComponent<AIPath>();
        aiPath.canMove = false;
        Player = FindAnyObjectByType<PlayerMovement>().gameObject;
        dest = GetComponent<AIDestinationSetter>();
        dest.target = Player.transform;
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
