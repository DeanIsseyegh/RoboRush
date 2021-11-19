using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    private GameObject player;

    public void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Update()
    {
        transform.position = new Vector3(player.transform.position.x + 0.5f, player.transform.position.y + 2, -0.2f);
    }

    private void OnTriggerEnter(Collider other)
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
