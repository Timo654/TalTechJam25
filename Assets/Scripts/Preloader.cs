using UnityEngine;

public class Preloader : MonoBehaviour
{
    private void Awake()
    {
        if (BuildConsts.isMobile) Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
    }
}
