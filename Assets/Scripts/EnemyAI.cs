using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    bool hasFound;

    public Transform target;          
    public float followRange = 5f;
    public float returnRange = 1f;

    private Vector3 originalPosition;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        originalPosition = transform.position;

        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (hasFound && distanceToTarget <= followRange)
            FollowTarget();
        else
            ReturnToOriginalPosition();
    }

    private void FollowTarget()
    {
        navMeshAgent.destination = target.position;
    }

    private void ReturnToOriginalPosition()
    {
        float distanceToOriginal = Vector3.Distance(transform.position, originalPosition);

        if (distanceToOriginal > returnRange)
            navMeshAgent.destination = originalPosition;
        else
            navMeshAgent.ResetPath();
    }

    public void ActivateAttack()
    {
        hasFound = true;
        Debug.Log("Attack");
    }
}
