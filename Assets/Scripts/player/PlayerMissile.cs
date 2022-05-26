using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : PlayerProjectile
{
    
    [SerializeField] private float xBoundary = 30;

    protected override void Start()
    {
        base.Start();
        transform.position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y + 2, -0.2f);
    }

    void Update()
    {
        if (transform.position.x > xBoundary) Destroy(gameObject);
    }

}
