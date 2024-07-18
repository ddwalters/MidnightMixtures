using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    [SerializeField] private Potion waterPotion;
    [SerializeField] private Potion shadowPotion;
    [SerializeField] private Potion potionThree;
    [SerializeField] private Potion potionFour;

    public Potion WaterPotion => waterPotion;
    public Potion ShadowPotion => shadowPotion;
    public Potion PotionThree => potionThree;
    public Potion PotionFour => potionFour;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}