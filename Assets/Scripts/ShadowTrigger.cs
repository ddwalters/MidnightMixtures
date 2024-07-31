using UnityEngine;

public class ShadowTrigger : MonoBehaviour
{
    PlayerVisibility playerVis;

    private void Start()
    {
        playerVis = FindAnyObjectByType<PlayerVisibility>().GetComponent<PlayerVisibility>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        playerVis.EnterShadow();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player") || !playerVis.GetInShadow()) return;

        playerVis.ExitShadow();
    }

    public void ForceExitShadow()
    {
        if (!playerVis.GetInShadow()) return;

        playerVis.ExitShadow();
    }
}
