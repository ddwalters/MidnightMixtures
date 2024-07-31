using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class levelComplete : MonoBehaviour
{
    public GameObject[] goblins;
    public List<bool> cage;

    void Start()
    {
        foreach(var goblin in goblins)
        {
            cage.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateCageList()
    {
        for(int i=0; i<goblins.Length; i++)
        {
            GoblinCage Gob = goblins[i].GetComponentInChildren<GoblinCage>();
            bool freed = Gob.escaped;
            cage[i] = freed;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (cage.All(x => x))
            {
                Debug.Log("Level Complete!");
            }
        }
    }
}
