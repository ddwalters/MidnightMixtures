using UnityEngine;

public class TorchBreak : MonoBehaviour
{
    [SerializeField] BoxCollider2D coll;
    [SerializeField] GameObject light;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("water"))
        {
            Destroy(light);
        }
    }
}
