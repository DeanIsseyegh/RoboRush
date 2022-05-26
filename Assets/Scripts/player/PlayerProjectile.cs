using UnityEngine;

public abstract class PlayerProjectile : MonoBehaviour
{
    protected GameObject player;

    protected virtual void Start()
    {
        player = GameObject.Find("Player");
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject otherGameObj = other.gameObject;
            DeathManager deathManager = otherGameObj.GetComponent<DeathManager>();
            deathManager.Kill(0, 50);
        } 
        else if (other.CompareTag("Obstacle"))
        {
            GameObject otherGameObj = other.gameObject;
            DeathManager deathManager = otherGameObj.GetComponent<DeathManager>();
            deathManager.Kill(0, 10);
        }
    }
}
