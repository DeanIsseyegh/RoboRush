using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathManager : MonoBehaviour
{

    public GameObject deathParticle;
    public AudioClip deathSound;
    private AudioSource audioSource;
    private UI ui;
    private ScorePopupManager scorePopupManager;

    private void Start()
    {
        ui = GameObject.Find("GameUI").GetComponent<UI>();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        scorePopupManager = GameObject.Find("ScorePopupManager").GetComponent<ScorePopupManager>();
    }

    public void Kill(int healthChange, float scoreChange, bool withSound = true)
    {
        scorePopupManager.ShowScorePopup(gameObject.transform, scoreChange);
        if (withSound) audioSource.PlayOneShot(deathSound);
        GameObject deathParticle = Instantiate(this.deathParticle, transform.position, transform.rotation);
        ui.UpdateHealth(healthChange);

        ui.UpdateScore(scoreChange);
        Destroy(deathParticle, 5f);
        Destroy(gameObject);
    }

}
