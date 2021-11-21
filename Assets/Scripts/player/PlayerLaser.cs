using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : PlayerProjectile
{
    public void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y + 2, -0.2f);
    }
}
