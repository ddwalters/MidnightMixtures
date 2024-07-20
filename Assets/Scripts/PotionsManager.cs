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
    [SerializeField] GameObject potionSlotPrefab;

    [SerializeField] GameObject SelectedSlotPosition;
    [SerializeField] GameObject slotPositionOne;
    [SerializeField] GameObject slotPositionTwo;
    [SerializeField] GameObject slotPositionThree;

    [SerializeField] Sprite waterPotionSprite;
    [SerializeField] Sprite shadowPotionSprite;
    [SerializeField] Sprite flashTextureSprite;
    [SerializeField] Sprite explosionTextureSprite;

    Potion selectedPotion;
    Potion sidePotion1;
    Potion sidePotion2;
    Potion sidePotion3;

    Potion waterPotion;
    Potion shadowPotion;
    Potion flashPotion;
    Potion explosionPotion;

    private void Start()
    {
        waterPotion = new Potion(waterPotionSprite, PotionType.Water);
        shadowPotion = new Potion(shadowPotionSprite, PotionType.Shadow);
        flashPotion = new Potion(flashTextureSprite, PotionType.Flash);
        explosionPotion = new Potion(explosionTextureSprite, PotionType.Explosion);
    }

    public void AddPotion(Potion potion)
    {
        if (!potion.hasCrafted)
        {
            var newPotion = potionSlotPrefab;
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
                selectedPotion = potion;
                newPotionSlot = Instantiate(newPotion, SelectedSlotPosition.transform);
            }
            else if (slotPositionOne.transform.childCount == 0)
            {
                sidePotion1 = potion;
                newPotionSlot = Instantiate(newPotion, slotPositionOne.transform);
            }
            else if (slotPositionTwo.transform.childCount == 0)
            {
                sidePotion2 = potion;
                newPotionSlot = Instantiate(newPotion, slotPositionTwo.transform);
            }
            else if (slotPositionThree.transform.childCount == 0)
            {
                sidePotion3 = potion;
                newPotionSlot = Instantiate(newPotion, slotPositionThree.transform);
            }
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

    public void CyclePotion()
    {
        if (sidePotion1 == null || sidePotion2 == null || sidePotion3 == null || selectedPotion == null)
        {
            Debug.LogError("One or more potions are null.");
            return;
        }

        // Store references to the potions
        Potion tempSelectedPotion = selectedPotion;
        Potion tempSidePotion1 = sidePotion1;
        Potion tempSidePotion2 = sidePotion2;
        Potion tempSidePotion3 = sidePotion3;

        // Cycle the potions
        selectedPotion = tempSidePotion1;
        sidePotion1 = tempSidePotion2;
        sidePotion2 = tempSidePotion3;
        sidePotion3 = tempSelectedPotion;

        // update ui ???
        Debug.Log(selectedPotion.potionType);
    }

    private void SwapPotionHelper(Potion newSelection)
    {
        if (newSelection == null)
        {
            Debug.LogError("New selection potion is null.");
            return;
        }

        Image currentChildImage = selectedPotion.slotObject.transform.GetChild(0).GetComponent<Image>();
        if (currentChildImage == null)
        {
            Debug.LogError("No Image component found on the child GameObject of the selected potion.");
            return;
        }

        TextMeshProUGUI currentChildText = selectedPotion.slotObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
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
        GameObject tempSlotObject = selectedPotion.slotObject;
        selectedPotion.slotObject = newSelection.slotObject;
        newSelection.slotObject = tempSlotObject;

        // Update the SelectedPotion reference
        selectedPotion = newSelection;
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
                flashPotion.stackCount--;
                childText.text = $"{flashPotion.stackCount}";
                break;
            case PotionType.Explosion:
                explosionPotion.stackCount--;
                childText.text = $"{explosionPotion.stackCount}";
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
