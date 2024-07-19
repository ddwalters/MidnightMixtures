using System.Collections;
using UnityEngine;

public class PotionThrow : MonoBehaviour
{
    [SerializeField] GameObject potionPrefab; // The potion object to be thrown
    [SerializeField] GameObject reticlePrefab; // The reticle object to indicate where the potion will land
    [SerializeField] GameObject glassParticle;
    public float throwSpeed = 10f; // Speed of the thrown potion
    public float maxThrowDistance = 5f; // Maximum distance the potion can be thrown
    public float maxArcHeight = 2f; // Maximum height of the arc
    public float rotationSpeed = 360f; // Speed of rotation (degrees per second)
    private Vector2 targetPosition;
    private bool isDragging = false;
    private GameObject reticleInstance;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the click is on the character
            Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(clickPosition, transform.position) < 0.5f) // Assuming a small radius around the character
            {
                isDragging = true;
                ShowReticle();
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            // Update the target position and reticle position while dragging
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UpdateReticlePosition();
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            // Record the target position when the mouse button is released
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            ThrowPotion();
            HideReticle();
        }
    }

    void ShowReticle()
    {
        if (reticleInstance == null)
        {
            reticleInstance = Instantiate(reticlePrefab);
        }
        reticleInstance.SetActive(true);
        UpdateReticlePosition();
    }

    void UpdateReticlePosition()
    {
        // Calculate the direction and distance to the target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float distance = Vector2.Distance(targetPosition, transform.position);

        // Clamp the distance to the maximum throw distance
        if (distance > maxThrowDistance)
        {
            distance = maxThrowDistance;
            targetPosition = (Vector2)transform.position + direction * distance;
        }

        // Update the reticle position
        reticleInstance.transform.position = targetPosition;
    }

    void HideReticle()
    {
        if (reticleInstance != null)
        {
            reticleInstance.SetActive(false);
        }
    }

    void ThrowPotion()
    {
        // Calculate the direction and distance to the target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float distance = Vector2.Distance(targetPosition, transform.position);

        // Clamp the distance to the maximum throw distance
        if (distance > maxThrowDistance)
        {
            distance = maxThrowDistance;
            targetPosition = (Vector2)transform.position + direction * distance;
        }

        // Calculate the arc height based on the throw distance
        float arcHeight = Mathf.Min(maxArcHeight, maxArcHeight * (distance / maxThrowDistance));

        // Instantiate the potion at the character's position
        GameObject potion = Instantiate(potionPrefab, transform.position, Quaternion.identity);

        // Move the potion towards the target position with an arc
        StartCoroutine(MovePotionWithArc(potion.transform, targetPosition, throwSpeed, arcHeight));
    }

    public IEnumerator MovePotionWithArc(Transform obj, Vector3 endPoint, float speed, float arcHeight)
    {
        Vector3 start = obj.position;
        Vector3 end = endPoint;
        float distance = Vector3.Distance(start, end);
        float elapsedTime = 0;
        Vector3 originalScale = obj.localScale;

        while (elapsedTime * speed < distance)
        {
            float t = (elapsedTime * speed) / distance;
            float height = Mathf.Sin(Mathf.PI * t) * arcHeight;
            obj.position = Vector3.Lerp(start, end, t) + new Vector3(0, height, 0);

            // Rotate the potion

            obj.Rotate(0, 0, rotationSpeed * Time.deltaTime);

            // Scale the potion
            float scale = Mathf.Lerp(0.8f, 1.2f, Mathf.Sin(Mathf.PI * t));
            obj.localScale = originalScale * scale;

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.position = end; // Ensure the final position is set to the end position

        // Break the potion
        BreakPotion(obj.gameObject);
    }

    void BreakPotion(GameObject potion)
    {
        // Implement the logic to break the potion here
        // For example, instantiate a break effect and destroy the potion
        Instantiate(glassParticle, potion.transform.position, Quaternion.identity);
        Destroy(potion);
    }
}
