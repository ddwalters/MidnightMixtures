using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum PotionType
{
    Water = 1,
    Shadow = 2,
    Flash = 3,
    Explosion = 4
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

    Potion SelectedPotion;

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

            Image childImage = newPotion.transform.GetChild(0).GetComponent<Image>();
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
            {
                SelectedPotion = potion;
                newPotionSlot = Instantiate(newPotion, SelectedSlotPosition.transform);
            }
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
            Image childImage = potion.slotObject.transform.GetChild(0).GetComponent<Image>();
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

    public void SelectPotion(PotionType type)
    {
        switch (type)
        {
            case PotionType.Water:
                SwapPotionHelper(waterPotion);
                break;
            case PotionType.Shadow:
                SwapPotionHelper(shadowPotion);
                break;
            case PotionType.Flash:
                SwapPotionHelper(flashPotion);
                break;
            case PotionType.Explosion:
                SwapPotionHelper(explosionPotion);
                break;
            default:
                Debug.Log("Definitly broken homie");
                return;
        }
    }

    private void SwapPotionHelper(Potion newSelection)
    {
        if (newSelection == null)
        {
            Debug.LogError("New selection potion is null.");
            return;
        }

        Image currentChildImage = SelectedPotion.slotObject.transform.GetChild(0).GetComponent<Image>();
        if (currentChildImage == null)
        {
            Debug.LogError("No Image component found on the child GameObject of the selected potion.");
            return;
        }

        TextMeshProUGUI currentChildText = SelectedPotion.slotObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (currentChildText == null)
        {
            Debug.LogError("No TextMeshPro component found on the child GameObject of the selected potion.");
            return;
        }

        Image newChildImage = newSelection.slotObject.transform.GetChild(0).GetComponent<Image>();
        if (newChildImage == null)
        {
            Debug.LogError("No Image component found on the child GameObject of the new selection.");
            return;
        }

        TextMeshProUGUI newChildText = newSelection.slotObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        if (newChildText == null)
        {
            Debug.LogError("No TextMeshPro component found on the child GameObject of the new selection.");
            return;
        }

        // Swap the UI elements
        Sprite tempSprite = currentChildImage.sprite;
        string tempText = currentChildText.text;

        currentChildImage.sprite = newChildImage.sprite;
        currentChildText.text = newChildText.text;

        newChildImage.sprite = tempSprite;
        newChildText.text = tempText;

        // Swap the slotObject references
        GameObject tempSlotObject = SelectedPotion.slotObject;
        SelectedPotion.slotObject = newSelection.slotObject;
        newSelection.slotObject = tempSlotObject;

        // Update the SelectedPotion reference
        SelectedPotion = newSelection;
    }

    public bool UsePotion()
    {
        var currentSelectedPotion = waterPotion;

        if (currentSelectedPotion.stackCount <= 0)
        {
            Debug.Log("Out of potions");
            return false;
        }

        Image childImage = currentSelectedPotion.slotObject.transform.GetChild(0).GetComponent<Image>();
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

    public void TestWaterButton() => AddPotion(waterPotion);

    public void TestShadowButton() => AddPotion(shadowPotion);

    public void TestFlashButton() => AddPotion(flashPotion);

    public void TestExplosionButton() => AddPotion(explosionPotion);

    public void SelectWater() => SelectPotion(PotionType.Water);

    public void SelectShadow() => SelectPotion(PotionType.Shadow);

    public void SelectFlash () => SelectPotion(PotionType.Flash);

    public void SelectExplosion() => SelectPotion(PotionType.Explosion);
}
