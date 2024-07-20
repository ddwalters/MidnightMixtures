using System;
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
    [SerializeField] GameObject potionSlotPrefab;

    [SerializeField] GameObject[] slotPositions;

    [SerializeField] Sprite waterPotionSprite;
    [SerializeField] Sprite shadowPotionSprite;
    [SerializeField] Sprite flashTextureSprite;
    [SerializeField] Sprite explosionTextureSprite;

    Potion selectedPotion;
    List<Potion> potions;

    Potion waterPotion;
    Potion shadowPotion;
    Potion flashPotion;
    Potion explosionPotion;

    private void Start()
    {
        potions = new List<Potion>(4);

        waterPotion = new Potion(waterPotionSprite, PotionType.Water);
        shadowPotion = new Potion(shadowPotionSprite, PotionType.Shadow);
        flashPotion = new Potion(flashTextureSprite, PotionType.Flash);
        explosionPotion = new Potion(explosionTextureSprite, PotionType.Explosion);

        potions.Add(waterPotion);
        potions.Add(shadowPotion);
        potions.Add(flashPotion);
        potions.Add(explosionPotion);
    }

    public void AddPotion(Potion potion)
    {
        if (!potion.hasCrafted)
        {
            int latestIndexWithStackCount = -1;
            for (int i = 0; i < potions.Count; i++)
            {
                if (potions[i].stackCount > 0)
                    latestIndexWithStackCount = i;
            }

            for (int i = 0; i < slotPositions.Length; i++)
            {
                if (slotPositions[i].transform.childCount == 0)
                {
                    GameObject newPotionSlot = Instantiate(potionSlotPrefab, slotPositions[i].transform);
                    potion.slotObject = newPotionSlot;
                    potion.hasCrafted = true;
                    potion.stackCount++;

                    if (latestIndexWithStackCount >= 0 && latestIndexWithStackCount < potions.Count - 1)
                        potions.Insert(latestIndexWithStackCount + 1, potion);
                    else
                        potions.Add(potion);

                    break;
                }
            }

            if (selectedPotion == null)
                selectedPotion = potions[0];
        }
        else
            potion.stackCount++;

        UpdatePotionUI();
    }

    public void SelectPotion(PotionType type)
    {
        Potion potionToSelect = potions.Find(potion => potion.potionType == type);
        if (potionToSelect != null)
            SwapPotionHelper(potionToSelect);
        else
            Debug.Log("Definitely broken homie");
    }

    public void CyclePotion()
    {
        int lastIndexWithCount = -1;
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].stackCount > 0)
                lastIndexWithCount = i;
        }

        if (lastIndexWithCount == -1)
        {
            Debug.LogError("No potion with stackCount > 0 found.");
            return;
        }

        Potion firstPotion = potions[0];
        for (int i = 1; i < potions.Count; i++)
            potions[i - 1] = potions[i];

        potions[Math.Min(lastIndexWithCount + 1, potions.Count - 1)] = firstPotion;

        UpdatePotionUI();
    }

    private void SwapPotionHelper(Potion newSelection)
    {
        if (newSelection == null)
        {
            Debug.LogError("New selection potion is null.");
            return;
        }

        // Swap the slotObject references
        GameObject tempSlotObject = selectedPotion.slotObject;
        selectedPotion.slotObject = newSelection.slotObject;
        newSelection.slotObject = tempSlotObject;

        // Update the SelectedPotion reference
        selectedPotion = newSelection;

        UpdatePotionUI();
    }

    public bool UsePotion()
    {
        selectedPotion.stackCount--;
        UpdatePotionUI();

        return true;
    }

    private void UpdatePotionUI()
    {
        foreach (Potion potion in potions)
        {
            if (!potion.hasCrafted)
                continue;

            if (potion.slotObject == null)
            {
                Debug.LogError("Potion slot object is null.");
                continue;
            }

            Image childImage = potion.slotObject.transform.GetChild(0).GetComponent<Image>();
            if (childImage == null)
            {
                Debug.LogError("No Image component found on the child GameObject.");
                continue;
            }

            TextMeshProUGUI childText = potion.slotObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            if (childText == null)
            {
                Debug.LogError("No TextMeshPro component found on the child GameObject.");
                continue;
            }

            childImage.sprite = potion.potionTexture;
            childText.text = $"{potion.stackCount}";
        }
    }

    public void TestWaterButton() => AddPotion(waterPotion);

    public void TestShadowButton() => AddPotion(shadowPotion);

    public void TestFlashButton() => AddPotion(flashPotion);

    public void TestExplosionButton() => AddPotion(explosionPotion);

    public void SelectWater() => SelectPotion(PotionType.Water);

    public void SelectShadow() => SelectPotion(PotionType.Shadow);

    public void SelectFlash() => SelectPotion(PotionType.Flash);

    public void SelectExplosion() => SelectPotion(PotionType.Explosion);
}