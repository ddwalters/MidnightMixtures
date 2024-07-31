using UnityEngine;

public class PlayerVisibility : MonoBehaviour
{
    bool inShadow;
    int inShadowLightCount = 0;

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
        if (inShadow)
        {
            inShadowLightCount++;
            visibility = 0;
            return;
        }

        if (visibility >= maxVisibility) return;
        visibility++;
    }

    public void DecreaseVisibility()
    {
        if (inShadow)
        {
            if(inShadowLightCount >= 0)
                inShadowLightCount--;
            visibility = 0;
            return;
        }

        if (visibility <= 1) return;

        visibility--;
    }

    public int GetVisibility()
    {
        return visibility;
    }

    public bool GetInShadow() => inShadow;

    public void EnterShadow()
    {
        inShadow = true;
    }

    public void ExitShadow()
    {
        inShadow = false;

        if (inShadowLightCount <= 1) return;

        visibility = inShadowLightCount;

        if (visibility > 3)
            visibility = 3;

        inShadowLightCount = 0;
    }
}
