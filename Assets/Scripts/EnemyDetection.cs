using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    PlayerVisibility playerVis;
    EnemyAI ai;

    bool inSight;

    [SerializeField]
    float baseNoticeTimer = 5f;

    float copyTimer;

    [SerializeField]
    float speedMultiplier = 1.5f;

    private void Start()
    {
        playerVis = FindAnyObjectByType<PlayerVisibility>().GetComponent<PlayerVisibility>();
        ai = gameObject.GetComponentInParent<EnemyAI>();

        copyTimer = baseNoticeTimer;
    }

    private void Update()
    {
        if (inSight)
        {
            var currentVis = playerVis.GetVisibility();
            copyTimer -= Time.deltaTime * (currentVis);
            Debug.Log(copyTimer);
            
            if (copyTimer < 0)
            {
                Debug.Log("In Sight");

                ai.ActivateMovement();
            }
        }
        else
        {
            copyTimer = baseNoticeTimer;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        inSight = true;
        Debug.Log("In Sight");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        inSight = false;
    }
}
