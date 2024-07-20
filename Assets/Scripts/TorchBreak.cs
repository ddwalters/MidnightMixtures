using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBreak : MonoBehaviour
{
    [SerializeField] BoxCollider2D coll;
    [SerializeField] GameObject light;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("water"))
        {
            Destroy(light);
        }
    }
}
