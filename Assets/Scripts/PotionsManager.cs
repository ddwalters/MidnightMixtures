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

    [SerializeField] GameObject SelectedSlotPosition;
    [SerializeField] GameObject SlotPositionOne;
    [SerializeField] GameObject SlotPositionTwo;
    [SerializeField] GameObject SlotPositionThree;

    [SerializeField] Potion waterPotion;
    [SerializeField] Potion shadowPotion;
    [SerializeField] Potion potionThree;
    [SerializeField] Potion potionFour;

    public void AddPotion(Potion potion)
    {
        if (!potion.hasCrafted)
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

            if (potion == waterPotion)
            {
                waterPotion.stackCount++;

                childImage.texture = waterPotion.potionTexture;
                childText.text = $"{waterPotion.stackCount}";
            }

            if (potion == shadowPotion)
            {
                shadowPotion.stackCount++;

                childImage.texture = shadowPotion.potionTexture;
                childText.text = $"{shadowPotion.stackCount}";
            }

            potion.hasCrafted = true;
            GameObject newPotionSlot = null;
            if (SelectedSlotPosition.transform.childCount == 0)
                newPotionSlot = Instantiate(newPotion, SelectedSlotPosition.transform);
            else if (SlotPositionOne.transform.childCount == 0)
                newPotionSlot = Instantiate(newPotion, SlotPositionOne.transform);
            else if (SlotPositionTwo.transform.childCount == 0)
                newPotionSlot = Instantiate(newPotion, SlotPositionTwo.transform);
            else if (SlotPositionThree.transform.childCount == 0)
                newPotionSlot = Instantiate(newPotion, SlotPositionThree.transform);
            else
                Debug.Log("Game's broken broski");

            if (newPotionSlot != null)
                potion.slotObject = newPotionSlot;
        }
        else
        {
            RawImage childImage = potion.slotObject.transform.GetChild(0).GetComponent<RawImage>();
            if (childImage == null)
            {
                Debug.LogError("No Image component found on the child GameObject.");
                return;
            }

            TextMeshProUGUI childText = potion.slotObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            if (childText == null)
            {
                Debug.LogError("No TextMeshPro component found on the child GameObject.");
                return;
            }

            if (potion == waterPotion)
            {
                waterPotion.stackCount++;
                childText.text = $"{waterPotion.stackCount}";
            }

            if (potion == shadowPotion)
            {
                shadowPotion.stackCount++;
                childText.text = $"{shadowPotion.stackCount}";
            }
        }
    }

    public bool UsePotion()
    {
        if (waterPotion.stackCount <= 0)
        {
            Debug.Log("Out of potions");
            return false;
        }

        RawImage childImage = waterPotion.slotObject.transform.GetChild(0).GetComponent<RawImage>();
        if (childImage == null)
        {
            Debug.LogError("No Image component found on the child GameObject.");
            return false;
        }

        TextMeshProUGUI childText = waterPotion.slotObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (childText == null)
        {
            Debug.LogError("No TextMeshPro component found on the child GameObject.");
            return false;
        }

        if (waterPotion == waterPotion)
        {
            waterPotion.stackCount--;
            childText.text = $"{waterPotion.stackCount}";
        }

        if (waterPotion == shadowPotion)
        {
            shadowPotion.stackCount--;
            childText.text = $"{shadowPotion.stackCount}";
        }

        return true;
    }

    public void TestShadowButton()
    {
        AddPotion(shadowPotion);
    }

    public void TestWaterButton()
    {
        AddPotion(waterPotion);
    }
}
