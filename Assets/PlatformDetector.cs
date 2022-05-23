using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    [SerializeField] private int androidTargetFramerate = 60;
    [SerializeField] private GameObject mobileControls;
    [SerializeField] private GameObject titleMobileControlsExplanation;
    [SerializeField] private GameObject pauseMobileControlsExplanation;
    [SerializeField] private GameObject titlePCControlsExplanation;
    [SerializeField] private GameObject pausePCControlsExplanation;

    private void Awake()
    {
        mobileControls.SetActive(false);
        titleMobileControlsExplanation.SetActive(false);
        pauseMobileControlsExplanation.SetActive(false);

# if UNITY_ANDROID
        Application.targetFrameRate = androidTargetFramerate;
        mobileControls.SetActive(true);
        titleMobileControlsExplanation.SetActive(true);
        pauseMobileControlsExplanation.SetActive(true);
        titlePCControlsExplanation.SetActive(false);
        pausePCControlsExplanation.SetActive(false);
# endif
    }
}