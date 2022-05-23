using UnityEngine;
using UnityEngine.UI;

public class MissileBar : MonoBehaviour
{
    public Slider missileBar;
    public Image mobileMissileCooldown;

    private float maxAmmo = 100;
    private float currentAmmo;

    public static MissileBar instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentAmmo = maxAmmo;
        missileBar.maxValue = maxAmmo;
        missileBar.value = maxAmmo;
    }

    public void UseAmmo()
    {
        currentAmmo = 0;
        missileBar.value = currentAmmo;
        mobileMissileCooldown.fillAmount = 1;
    }

    public void RefillAmmo(float percentToRefillBy)
    {
        currentAmmo = maxAmmo * percentToRefillBy;
        missileBar.value = currentAmmo;
        mobileMissileCooldown.fillAmount = 1 - percentToRefillBy;
    }
}
