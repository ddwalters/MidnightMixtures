using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    bool inShadow;

    [SerializeField]
    private int maxVisibility;
    [SerializeField]
    private int visibility = 1;

    private void Start()
    {
        inShadow = false;
    }

    public void IncreaseVisibility()
    {
        if (visibility >= maxVisibility) return;
        visibility++;
    }

    public void DecreaseVisibility()
    {
        if (visibility <= 1) return;

        if (inShadow)
        {

            return;
        }

        visibility--;
    }

    public int GetVisibility()
    {
        return visibility;
    }

    public void EnterShadow()
    {
        inShadow = false;
    }
}
