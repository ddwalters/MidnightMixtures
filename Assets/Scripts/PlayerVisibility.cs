using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    bool inShadow;

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
