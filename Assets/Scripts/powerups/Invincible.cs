using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincible : Powerup
{

    private PlayerController playerController;

    protected override void Start()
    {
        base.Start();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    protected override void applyPowerup()
    {
        playerController.MakeInvincible(5f);
    }

}
