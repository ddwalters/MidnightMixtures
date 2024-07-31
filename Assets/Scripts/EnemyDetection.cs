using UnityEngine;
using UnityEngine.UI;

public class EnemyDetection : MonoBehaviour
{
    PlayerVisibility playerVis;
    EnemyAI ai;

    [SerializeField] Slider noticeSlider;
    [SerializeField] float baseNoticeTimer = 5f;
    float copyTimer;

    bool inSight;

    private void Start()
    {
        playerVis = FindAnyObjectByType<PlayerVisibility>().GetComponent<PlayerVisibility>();
        ai = gameObject.GetComponentInParent<EnemyAI>();
        noticeSlider = FindObjectOfType<Slider>();

        copyTimer = baseNoticeTimer;
        noticeSlider.value = 0;
    }

    private void Update()
    {
        if (inSight)
        {
            var currentVis = playerVis.GetVisibility();

            if (currentVis > 0)
            {
                copyTimer -= Time.deltaTime * currentVis;
                noticeSlider.value = Mathf.Clamp01(1 - (copyTimer / baseNoticeTimer));

                if (copyTimer <= 0)
                {
                    ai.ActivateMovement();
                    copyTimer = baseNoticeTimer;
                }
            }
        }

        if (copyTimer != baseNoticeTimer && !inSight)
        {
            copyTimer = Mathf.Lerp(copyTimer, baseNoticeTimer, Time.deltaTime * 1.5f);
            noticeSlider.value = Mathf.Clamp01(1 - (copyTimer / baseNoticeTimer));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            inSight = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            inSight = false;
    }
}
