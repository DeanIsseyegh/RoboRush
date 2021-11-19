using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Powerup
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void applyPowerup()
    {
        DeathManager.KillAllWithEffectsButNoSound();
    }
}
