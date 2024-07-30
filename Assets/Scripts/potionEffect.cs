using System.Collections;
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
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void potionBreak(GameObject potion)
    {
        if (state == Effect.water)
        {
            GameObject splash = Instantiate(waterPotion.v1, potion.transform.position, Quaternion.identity);
            StartCoroutine(FadeAndDestroy(splash, waterPotion.v2));
        }
    }

    private IEnumerator FadeAndDestroy(GameObject splash, float duration)
    {
        SpriteRenderer spriteRenderer = splash.GetComponent<SpriteRenderer>();
        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        Destroy(splash);
    }

}
