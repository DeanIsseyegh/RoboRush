using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerProjectile : MonoBehaviour
{
    protected GameObject player;

    virtual protected void Start()
    {
        player = GameObject.Find("Player");
    }

    virtual protected void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            DeathManager deathManager = other.gameObject.GetComponent<DeathManager>();
            deathManager.Kill(0, 50);
        } 
        else if (other.CompareTag("Obstacle"))
        {
            DeathManager deathManager = other.gameObject.GetComponent<DeathManager>();
            deathManager.Kill(0, 10);
        }
    }
}
