using UnityEngine;

public class LightDetector2D : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D torchLight;
    public float lightThreshold = 0.5f; // Adjust this value based on your light's intensity
    private Transform playerTransform;
    private Collider2D playerCollider;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerCollider = other;
            CheckIfPlayerInBrightLight();
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerTransform = other.transform;
            playerCollider = other;
            CheckIfPlayerInBrightLight();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the light area.");
            playerTransform = null;
            playerCollider = null;
            // Add your logic here to handle the player leaving the bright light
        }
    }

    private void CheckIfPlayerInBrightLight()
    {
        Bounds bounds = playerCollider.bounds;

        Vector2[] checkPoints = new Vector2[]
        {
            bounds.center,
            bounds.min,
            new Vector2(bounds.min.x, bounds.max.y),
            bounds.max,
            new Vector2(bounds.max.x, bounds.min.y)
        };

        foreach (var point in checkPoints)
        {
            Vector2 directionToPoint = point - (Vector2)torchLight.transform.position;
            RaycastHit2D hit = Physics2D.Raycast(torchLight.transform.position, directionToPoint, torchLight.pointLightOuterRadius);

            if (hit.collider != null && hit.collider == playerCollider)
            {
                Debug.Log("raycast: hit");
                float lightIntensity = GetLightIntensityAtPoint(playerTransform.position);
                Debug.Log("light: " + lightIntensity);
                if (lightIntensity > lightThreshold)
                {
                    Debug.Log("Player is in bright light!");
                    // Add your logic here to handle the player being in bright light
                    break;
                }
            }
        }
    }

    private float GetLightIntensityAtPoint(Vector2 point)
    {
        // Calculate the distance from the light to the point
        float distance = Vector2.Distance(torchLight.transform.position, point);

        // If the point is within the light's range, calculate the intensity
        if (distance < torchLight.pointLightOuterRadius)
        {
            // Light intensity decreases with the square of the distance
            float intensity = torchLight.intensity / (distance * distance);
            return intensity;
        }

        // If the point is outside the light's range, return 0
        return 0;
    }

    private void OnDrawGizmos()
    {
        if (playerTransform != null && playerCollider != null)
        {
            Bounds bounds = playerCollider.bounds;

            Vector2[] checkPoints = new Vector2[]
            {
                bounds.center,
                bounds.min,
                new Vector2(bounds.min.x, bounds.max.y),
                bounds.max,
                new Vector2(bounds.max.x, bounds.min.y)
            };

            Gizmos.color = Color.red;
            foreach (var point in checkPoints)
            {
                Gizmos.DrawLine(torchLight.transform.position, point);
            }

            Gizmos.color = Color.green;
            foreach (var point in checkPoints)
            {
                Vector2 directionToPoint = point - (Vector2)torchLight.transform.position;
                RaycastHit2D hit = Physics2D.Raycast(torchLight.transform.position, directionToPoint, torchLight.pointLightOuterRadius);

                if (hit.collider != null && hit.collider == playerCollider)
                {
                    Gizmos.DrawLine(torchLight.transform.position, hit.point);
                }
            }
        }
    }
}
