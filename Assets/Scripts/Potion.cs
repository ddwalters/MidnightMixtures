using UnityEngine;

public class Potion
{
    public GameObject potionPrefab;
    public Sprite potionTexture;
    public GameObject slotObject;
    public PotionType potionType;
    public int stackCount = 0;
    public bool hasCrafted = false;

    public Potion(GameObject prefab, Sprite texture, PotionType type)
    {
        potionPrefab = prefab;
        potionTexture = texture;
        potionType = type;
    }
}