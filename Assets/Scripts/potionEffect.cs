using SerializedTuples;
using SerializedTuples.Runtime;
using System.Collections;
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

    [SerializedTupleLabels("waterSplash", "time")]
    public SerializedTuple<GameObject, float, Sprite> waterPotion;

    [SerializedTupleLabels("shadowPotion", "time")]
    public SerializedTuple<GameObject, float, Sprite> shadowPotion;

    [SerializedTupleLabels("flashPotion", "time")]
    public SerializedTuple<GameObject, float, Sprite> flashPotion;

    [SerializedTupleLabels("explosionPotion", "time")]
    public SerializedTuple<GameObject, float, Sprite> explosionPotion;

    void Start()
    {
        Time.timeScale = 1.0f;
    }

    public float potionBreak(GameObject potion)
    {
        float fadeTime = 1f;
        Destroy(potion.GetComponent<SpriteRenderer>());
        Destroy(potion.GetComponent<TrailRenderer>());

        if (state == Effect.water)
        {
            GameObject splash = Instantiate(waterPotion.v1, potion.transform.position, Quaternion.identity);
            StartCoroutine(FadeAndDestroy(splash, waterPotion.v2));
            fadeTime = waterPotion.v2;
        }

        if (state == Effect.shadow)
        {
            GameObject shadow = Instantiate(shadowPotion.v1, potion.transform.position, Quaternion.identity);
            StartCoroutine(FadeAndDestroy(shadow, shadowPotion.v2));
            fadeTime = shadowPotion.v2;
        }

        if (state == Effect.flash)
        {
            GameObject flash = Instantiate(flashPotion.v1, potion.transform.position, Quaternion.identity);
            StartCoroutine(FadeAndDestroy(flash, flashPotion.v2));
            fadeTime = flashPotion.v2;
        }

        if (state == Effect.explosion)
        {
            GameObject boom = Instantiate(explosionPotion.v1, potion.transform.position, Quaternion.identity);
            StartCoroutine(FadeAndDestroy(boom, explosionPotion.v2));
            fadeTime = explosionPotion.v2;
        }

        return fadeTime;
    }

    private IEnumerator FadeAndDestroy(GameObject splash, float duration)
    {
        SpriteRenderer spriteRenderer = splash.GetComponent<SpriteRenderer>();

        Color originalColor = spriteRenderer.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration - 0.5f)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        if (state == Effect.shadow)
        {
            var shadowTrigger = splash.GetComponent<ShadowTrigger>();
            shadowTrigger.ForceExitShadow();
        }

        Destroy(splash);
    }
}
