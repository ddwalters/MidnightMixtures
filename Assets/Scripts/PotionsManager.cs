using SerializedTuples;
using SerializedTuples.Runtime;
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

    [SerializedTupleLabels("Sprite", "Prefab", "EmptySprite")]
    public SerializedTuple<Sprite, GameObject, Sprite> waterPotion;
    [SerializedTupleLabels("Sprite", "Prefab")]
    public SerializedTuple<Sprite, GameObject> shadowPotion;
    [SerializedTupleLabels("Sprite", "Prefab")]
    public SerializedTuple<Sprite, GameObject> flashPotion;
    [SerializedTupleLabels("Sprite", "Prefab")]
    public SerializedTuple<Sprite, GameObject> explosionPotion;

    Potion selectedPotion;
    List<Potion> potions;

    private void Start()
    {
        potions = new List<Potion>();
    }

    public void AddPotion(PotionType potionType)
    {
        Potion potion = potions.Find(p => p.potionType == potionType);

        if (potion == null)
        {
            if (potions.Count >= 4)
            {
                Debug.Log("Max of 4 potion types");
                return;
            }

            GameObject potionPrefab = null;
            Sprite potionSprite = null;
            switch (potionType)
            {
                case PotionType.Water:
                    potionSprite = waterPotion.v1;
                    potionPrefab = waterPotion.v2;
                    break;
                case PotionType.Shadow:
                    potionSprite = shadowPotion.v1;
                    potionPrefab = shadowPotion.v2;
                    break;
                case PotionType.Flash:
                    potionSprite = flashPotion.v1;
                    potionPrefab = flashPotion.v2;
                    break;
                case PotionType.Explosion:
                    potionSprite = explosionPotion.v1;
                    potionPrefab = explosionPotion.v2;
                    break;
                default:
                    Debug.Log("Unexcepted potion type");
                    return;
            }

            if (potionSprite == null || potionPrefab == null)
            {
                Debug.Log("Missing potion sprite or prefab");
                return;
            }

            potion = new Potion(potionPrefab, potionSprite, potionType);
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
                selectedPotion = potion;
        }
        else
        {
            if (potionType == PotionType.Water)
                FillWater();
            else
                potion.stackCount++;
        }

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

    public GameObject GetSelectedPotionPrefab() => selectedPotion.potionPrefab;

    public void CyclePotion()
    {
        if (potions.Count == 0) return;

        Potion firstPotion = potions[0];
        for (int i = 1; i < potions.Count; i++)
            potions[i - 1] = potions[i];

        int lastIndexWithCount = -1;
        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].stackCount > 0)
                lastIndexWithCount = i;
        }

        potions[Math.Min(lastIndexWithCount + 1, potions.Count - 1)] = firstPotion;

        for (int i = 0; i < potions.Count; i++)
        {
            if (potions[i].slotObject != null)
                potions[i].slotObject.transform.SetParent(slotPositions[i].transform, false);
        }

        selectedPotion = potions[0];

        UpdatePotionUI();
    }

    public bool UsePotion()
    {
        if (selectedPotion == null || selectedPotion.stackCount <= 0) return false;

        if (selectedPotion.potionType == PotionType.Water)
        {
            selectedPotion.potionTexture = waterPotion.v3;

            if (selectedPotion.stackCount > 1)
            {
                selectedPotion.stackCount = 0;
                UpdatePotionUI();

                return true;
            }
        }

        selectedPotion.stackCount--;
        UpdatePotionUI();

        return true;
    }

    private void SwapPotionHelper(Potion newSelection)
    {
        if (newSelection == null)
        {
            Debug.LogError("New selection potion is null.");
            return;
        }

        GameObject tempSlotObject = selectedPotion.slotObject;
        selectedPotion.slotObject = newSelection.slotObject;
        newSelection.slotObject = tempSlotObject;

        selectedPotion = newSelection;

        UpdatePotionUI();
    }

    private void FillWater()
    {
        if (selectedPotion.stackCount > 0)
        {
            Debug.Log("Water is already filled");
            return;
        }

        selectedPotion.potionTexture = waterPotion.v1;
        selectedPotion.potionPrefab = waterPotion.v2;

        selectedPotion.stackCount++;
    }

    private void UpdatePotionUI()
    {
        for (int i = 0; i < potions.Count; i++)
        {
            Potion potion = potions[i];

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

            if (potion.potionType == PotionType.Water)
                childText.alpha = 0.0f;
            else
                childText.alpha = 1.0f;
        }
    }

    public void TestWaterButton() => AddPotion(PotionType.Water);
    public void TestShadowButton() => AddPotion(PotionType.Shadow);
    public void TestFlashButton() => AddPotion(PotionType.Flash);
    public void TestExplosionButton() => AddPotion(PotionType.Explosion);

    public void SelectWater() => SelectPotion(PotionType.Water);
    public void SelectShadow() => SelectPotion(PotionType.Shadow);
    public void SelectFlash() => SelectPotion(PotionType.Flash);
    public void SelectExplosion() => SelectPotion(PotionType.Explosion);
}