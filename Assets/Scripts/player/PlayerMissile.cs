using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : PlayerProjectile
{
    
    [SerializeField] private float xBoundary = 30;
    [SerializeField] private float playerXOffset = 0.5f;
    [SerializeField] private float playerYOffset = 2;
    [SerializeField] private float playerZOffset = -0.2f;

    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(player.transform.position.x + playerXOffset, player.transform.position.y + playerYOffset, playerZOffset);
    }

    void Update()
    {
        if (transform.position.x > xBoundary) Destroy(gameObject);
    }

}
