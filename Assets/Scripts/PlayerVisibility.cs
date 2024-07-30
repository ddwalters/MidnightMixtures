using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    [SerializeField]
    private int maxVisibility;
    [SerializeField]
    private int visibility = 1;

    public void IncreaseVisibility()
    {
        if (visibility >= maxVisibility) return;
        visibility++;
    }

    public void DecreaseVisibility()
    {
        if (visibility <= 1) return;
        visibility--;
    }

    public int GetVisibility()
    {
        return visibility;
    }
}
