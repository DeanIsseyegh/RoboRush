using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    [SerializeField] private int androidTargetFramerate = 60;
    [SerializeField] private GameObject mobileControls;

    private void Awake()
    {
        mobileControls.SetActive(false);
# if UNITY_ANDROID
        Application.targetFrameRate = androidTargetFramerate;
        mobileControls.SetActive(true);
# endif
    }
}