using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class GoblinCage : MonoBehaviour
{
    public bool escaped = false;
    private Transform exit;
    private levelComplete LevelComplete;
    public AIPath aiPath;
    public AIDestinationSetter dest;
    private SpriteRenderer sprite;
    public GameObject cage;

    private void Start()
    {
        LevelComplete = FindAnyObjectByType<levelComplete>();
        exit = FindAnyObjectByType<levelComplete>().transform;
        sprite = cage.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Goblin Freed!");
            escaped = true;
            LevelComplete.updateCageList();
            sprite.enabled = false;
            dest.target = exit;
            aiPath.canMove = true;
        }
    }

    private void Update()
    {
        if(escaped == true)
        {
            if(gameObject.transform == exit)
            {
                Destroy(this);
            }
        }
    }
}
