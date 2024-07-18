using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Potion")]
public class Potion : ScriptableObject
{
    [SerializeField] public Texture potionTexture;

    [DoNotSerialize] public PotionType type;

    [DoNotSerialize] public int stackCount = 0;
}
