using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightEffector : MonoBehaviour
{
    PlayerVisibility playerVis;

    void Start()
    {
        playerVis = FindAnyObjectByType<PlayerVisibility>().GetComponent<PlayerVisibility>();

        float lightRadius = GetComponent<Light2D>().pointLightOuterRadius;
        var circleRef = gameObject.AddComponent<CircleCollider2D>();
        circleRef.radius = lightRadius;
        circleRef.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        playerVis.IncreaseVisibility();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;

        playerVis.DecreaseVisibility();
    }
}
