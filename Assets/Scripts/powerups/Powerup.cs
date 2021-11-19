using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup : MonoBehaviour
{
    [SerializeField] private AudioClip powerUpSoundEffect;
    private AudioSource audioSource;

    protected virtual void Start()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            applyPowerup();
            audioSource.PlayOneShot(powerUpSoundEffect);
            Destroy(gameObject);
        }
    }

    protected abstract void applyPowerup();    
}
