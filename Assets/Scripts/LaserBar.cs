using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserBar : MonoBehaviour
{
    public Slider laserBar;

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
    }

    public void RefillLaserAmmo(float percentToRefillBy)
    {
        currentLaser = maxLaser * percentToRefillBy;
        laserBar.value = currentLaser;
    }

}
