using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBar : MonoBehaviour
{
    public Slider laserBar;
    public Image mobileLaserCooldown;

    private float maxLaser = 100;
    private float currentLaser;

    public static LaserBar instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentLaser = maxLaser;
        laserBar.maxValue = maxLaser;
        laserBar.value = maxLaser;
    }

    public void UseLaserAmmo()
    {
        currentLaser = 0;
        laserBar.value = currentLaser;
        mobileLaserCooldown.fillAmount = 1;
    }

    public void RefillLaserAmmo(float percentToRefillBy)
    {
        currentLaser = maxLaser * percentToRefillBy;
        laserBar.value = currentLaser;
        mobileLaserCooldown.fillAmount = 1 - percentToRefillBy;
    }

}
