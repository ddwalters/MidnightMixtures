using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class torchFlicker : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D light2D;
    private float baseIntensity;
    private float baseOuterRadius;

    // You can adjust these values to get the desired flicker effect
    [SerializeField] public float intensityFluctuation = 0.1f;
    [SerializeField] public float radiusFluctuation = 0.1f;
    [SerializeField] public float flickerSpeed = 0.1f;

    void Start()
    {
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        baseIntensity = light2D.intensity;
        baseOuterRadius = light2D.pointLightOuterRadius;
    }

    void Update()
    {
        // Apply slight random fluctuation to the intensity
        light2D.intensity = baseIntensity + Random.Range(-intensityFluctuation, intensityFluctuation);

        // Apply slight random fluctuation to the outer radius
        light2D.pointLightOuterRadius = baseOuterRadius + Random.Range(-radiusFluctuation, radiusFluctuation);

        // Control the speed of flickering
        light2D.intensity = Mathf.Lerp(light2D.intensity, baseIntensity, Time.deltaTime * flickerSpeed);
        light2D.pointLightOuterRadius = Mathf.Lerp(light2D.pointLightOuterRadius, baseOuterRadius, Time.deltaTime * flickerSpeed);
    }
}
