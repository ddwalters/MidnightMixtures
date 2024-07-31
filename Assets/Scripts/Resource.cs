using UnityEngine;

public class Resource : MonoBehaviour
{
    [SerializeField] PotionType resourceType;

    PotionsManager potionManager;
    GameObject player;

    private void Start()
    {
        potionManager = FindAnyObjectByType<PotionsManager>();
        player = FindAnyObjectByType<PlayerMovement>().gameObject;
    }

    private void Update()
    {
        if (Vector2.Distance(player.transform.position, transform.position) > 1.5f)
            return;

        Vector2 clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && Vector2.Distance(clickPosition, transform.position) < .75f)
        {
            potionManager.AddPotion(resourceType);
        }
    }
}
