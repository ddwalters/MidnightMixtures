using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PotionType
{
    Water,
    Shadow,
    Flash,
    Explosion
}

public class PotionsManager : MonoBehaviour
{
    [SerializeField] GameObject PotionSlotPrefab;

    [SerializeField] GameObject SelectedSlotPosition;
    [SerializeField] GameObject SlotPositionOne;
    [SerializeField] GameObject SlotPositionTwo;
    [SerializeField] GameObject SlotPositionThree;

    [SerializeField] Sprite waterPotionSprite;
    [SerializeField] Sprite shadowPotionSprite;
    [SerializeField] Sprite FlashTextureSprite;
    [SerializeField] Sprite ExplosionTextureSprite;

    Potion waterPotion;
    Potion shadowPotion;
    Potion flashPotion;
    Potion explosionPotion;

    private void Start()
    {
        waterPotion = new Potion(waterPotionSprite, PotionType.Water);
        shadowPotion = new Potion(shadowPotionSprite, PotionType.Shadow);
        flashPotion = new Potion(FlashTextureSprite, PotionType.Flash);
        explosionPotion = new Potion(ExplosionTextureSprite, PotionType.Explosion);
    }

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

            SpriteRenderer childImage = newPotion.transform.GetChild(0).GetComponent<SpriteRenderer>();
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

            switch (potion.potionType)
            {
                case PotionType.Water:
                    waterPotion.stackCount++;
                    childImage.sprite = waterPotion.potionTexture;
                    childText.text = $"{waterPotion.stackCount}";
                    break;
                case PotionType.Shadow:
                    shadowPotion.stackCount++;
                    childImage.sprite = shadowPotion.potionTexture;
                    childText.text = $"{shadowPotion.stackCount}";
                    break;
                case PotionType.Flash:
                    flashPotion.stackCount++;
                    childImage.sprite = flashPotion.potionTexture;
                    childText.text = $"{flashPotion.stackCount}";
                    break;
                case PotionType.Explosion:
                    explosionPotion.stackCount++;
                    childImage.sprite = explosionPotion.potionTexture;
                    childText.text = $"{explosionPotion.stackCount}";
                    break;
                default:
                    Debug.Log("Definitly broken homie");
                    return;
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
            SpriteRenderer childImage = potion.slotObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
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

            switch (potion.potionType)
            {
                case PotionType.Water:
                    waterPotion.stackCount++;
                    childText.text = $"{waterPotion.stackCount}";
                    break;
                case PotionType.Shadow:
                    shadowPotion.stackCount++;
                    childText.text = $"{shadowPotion.stackCount}";
                    break;
                case PotionType.Flash:
                    waterPotion.stackCount++;
                    childText.text = $"{waterPotion.stackCount}";
                    break;
                case PotionType.Explosion:
                    shadowPotion.stackCount++;
                    childText.text = $"{shadowPotion.stackCount}";
                    break;
                default:
                    Debug.Log("Definitly broken homie");
                    return;
            }
        }
    }

    public bool UsePotion()
    {
        var currentSelectedPotion = waterPotion;

        if (currentSelectedPotion.stackCount <= 0)
        {
            Debug.Log("Out of potions");
            return false;
        }

        SpriteRenderer childImage = currentSelectedPotion.slotObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (childImage == null)
        {
            Debug.LogError("No Image component found on the child GameObject.");
            return false;
        }

        TextMeshProUGUI childText = currentSelectedPotion.slotObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (childText == null)
        {
            Debug.LogError("No TextMeshPro component found on the child GameObject.");
            return false;
        }

        switch (currentSelectedPotion.potionType)
        {
            case PotionType.Water:
                waterPotion.stackCount--;
                childText.text = $"{waterPotion.stackCount}";
                break;
            case PotionType.Shadow:
                shadowPotion.stackCount--;
                childText.text = $"{shadowPotion.stackCount}";
                break;
            case PotionType.Flash:
                waterPotion.stackCount--;
                childText.text = $"{waterPotion.stackCount}";
                break;
            case PotionType.Explosion:
                shadowPotion.stackCount--;
                childText.text = $"{shadowPotion.stackCount}";
                break;
            default:
                Debug.Log("Definitly broken homie");
                return false;
        }

        return true;
    }

    public void TestWaterButton()
    {
        AddPotion(waterPotion);
    }

    public void TestShadowButton()
    {
        AddPotion(shadowPotion);
    }

    public void TestFlashButton()
    {
        AddPotion(flashPotion);
    }

    public void TestExplosionButton()
    {
        AddPotion(explosionPotion);
    }
}
