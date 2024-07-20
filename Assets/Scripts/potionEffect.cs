using System.Collections;
using System.Collections.Generic;
using SerializedTuples;
using SerializedTuples.Runtime;
using UnityEngine;

public class potionEffect : MonoBehaviour
{
    public enum Effect
    {
        water,
        flash,
        shadow,
        explosion
    }

    public Effect state;

    [SerializedTupleLabels("waterSplash", "time", "EmptySprite")]
    public SerializedTuple<GameObject, float, Sprite> waterPotion;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void potionBreak(GameObject potion)
    {
        if(state == Effect.water)
        {
            Instantiate(waterPotion.v1, potion.transform.position, Quaternion.Euler(0, 0, 0));
        }
    }
}
