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

    [SerializeField]
    float speedMultiplier = 1.5f;

    private void Start()
    {
        playerVis = FindAnyObjectByType<PlayerVisibility>().GetComponent<PlayerVisibility>();
        ai = gameObject.GetComponentInParent<EnemyAI>();
    }

    private void Update()
    {
        if (inSight)
            Notice();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        inSight = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        inSight = false;
    }

    private void Notice()
    {
        var currentVis = playerVis.GetVisibility();
        float timer = baseNoticeTimer;

        while (timer > 0 && inSight)
            timer -= Mathf.Exp(currentVis * speedMultiplier) * Time.deltaTime;

        if (!inSight)
            return;

        ai.ActivateAttack();
    }
}
