using UnityEngine;

public class Potion
{
    public Sprite potionTexture;
    public GameObject slotObject;
    public PotionType potionType;
    public int stackCount = 0;
    public bool hasCrafted = false;

    public Potion(Sprite texture, PotionType type)
    {
        potionTexture = texture;
        potionType = type;
    }
}