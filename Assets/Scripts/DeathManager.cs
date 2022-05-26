using System.Collections;
using System.Collections.Generic;
using objectpooling;
using TMPro;
using UnityEngine;

public class DeathManager : MonoBehaviour
{

    public GameObject deathParticle;
    public AudioClip deathSound;
    public string poolName;

    private AudioSource _audioSource;
    private UI _ui;
    private ScorePopupManager _scorePopupManager;
    private SimpleObjectPool _simpleObjectPool;

    private void Start()
    {
        _ui = GameObject.Find("GameUI").GetComponent<UI>();
        _audioSource = GameObject.Find("SoundEffects").GetComponent<AudioSource>();
        _scorePopupManager = GameObject.Find("ScorePopupManager").GetComponent<ScorePopupManager>();
        _simpleObjectPool = GameObject.FindGameObjectWithTag(poolName).GetComponent<SimpleObjectPool>();
    }

    public void Kill(int healthChange, float scoreChange, bool withSound = true)
    {
        _scorePopupManager.ShowScorePopup(gameObject.transform, scoreChange);
        if (withSound) _audioSource.PlayOneShot(deathSound);
        GameObject deathParticle = Instantiate(this.deathParticle, transform.position, transform.rotation);
        _ui.UpdateHealth(healthChange);

        _ui.UpdateScore(scoreChange);
        Destroy(deathParticle, 5f);
        _simpleObjectPool.Release(gameObject);
    }

    public static void KillAllWithEffectsButNoSound()
    {
        DestroyAllWithEffectsButNoSound(GameObject.FindGameObjectsWithTag("Enemy"));
        DestroyAllWithEffectsButNoSound(GameObject.FindGameObjectsWithTag("Obstacle"));
    }

    private static void DestroyAllWithEffectsButNoSound(GameObject[] gameObjects)
    {
        foreach (GameObject toDestroy in gameObjects)
        {
            toDestroy.GetComponent<DeathManager>().Kill(0, 50f, false);
        }
    }

    public static void KillAll() {
        DestroyAll(GameObject.FindGameObjectsWithTag("Enemy"));
        DestroyAll(GameObject.FindGameObjectsWithTag("Obstacle"));
        DestroyAll(GameObject.FindGameObjectsWithTag("Powerup"));
    }

    private static void DestroyAll(GameObject[] gameObjects)
    {
        foreach (GameObject toDestroy in gameObjects)
        {
            Destroy(toDestroy);
        }
    }

}
