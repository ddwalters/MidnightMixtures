using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Potion")]
public class Potion : ScriptableObject
{
    [SerializeField] public Texture potionTexture;

    public GameObject slotObject;

    public PotionType type;

    public int stackCount = 0;

    public bool hasCrafted = false;
}
