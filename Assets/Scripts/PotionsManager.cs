using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PotionType
{
    Water,
    Shadow,
    PotionThree,
    PotionFour
}

public class PotionsManager : MonoBehaviour
{
    [SerializeField] GameObject PotionSlotPrefab;

    [SerializeField] Transform SelectedSlotPosition;
    [SerializeField] Transform SlotPositionOne;
    [SerializeField] Transform SlotPositionTwo;
    [SerializeField] Transform SlotPositionThree;

    [SerializeField] Potion waterPotion;
    [SerializeField] Potion shadowPotion;
    [SerializeField] Potion potionThree;
    [SerializeField] Potion potionFour;

    public void AddPotion(Potion potion)
    {
        var newPotion = PotionSlotPrefab;
        if (newPotion == null)
        {
            Debug.LogError("slotWithPotion is not assigned.");
            return;
        }

        RawImage childImage = newPotion.transform.GetChild(0).GetComponent<RawImage>();
        if (childImage == null)
        {
            Debug.LogError("No Image component found on the child GameObject.");
            return;
        }

        TextMeshProUGUI childText = newPotion.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (childText == null)
        {
            Debug.LogError("No TextMeshPro component found on the child GameObject.");
            return;
        }

        if (potion == ResourceManager.Instance.WaterPotion)
        {
            ResourceManager.Instance.WaterPotion.stackCount++;

            childImage.texture = ResourceManager.Instance.WaterPotion.potionTexture;
            childText.text = $"{ResourceManager.Instance.WaterPotion.stackCount}";
        }

        if (potion == ResourceManager.Instance.ShadowPotion)
        {
            ResourceManager.Instance.ShadowPotion.stackCount++;

            childImage.texture = ResourceManager.Instance.ShadowPotion.potionTexture;
            childText.text = $"{ResourceManager.Instance.ShadowPotion.stackCount}";
        }

        Instantiate(newPotion, SelectedSlotPosition);
    }

    public void TestShadowButton()
    {
        AddPotion(ResourceManager.Instance.WaterPotion);
    }

    public void TestWaterButton()
    {
        AddPotion(ResourceManager.Instance.ShadowPotion);
    }
}
