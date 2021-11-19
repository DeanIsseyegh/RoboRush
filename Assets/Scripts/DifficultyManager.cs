using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public GameObject difficultyPopupPrefab;

    private SpawnManager spawnManager;
    private GameObject player;
    private Boolean isInGame = false;
    private float difficultyPopupLength = 3f;

    private float timeSinceLastDifficultyInc = 0f;
    private float difficultyIncreaseInterval = 7f;

    private void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (isInGame)
        {
            timeSinceLastDifficultyInc += Time.deltaTime;
            if (timeSinceLastDifficultyInc > difficultyIncreaseInterval)
            {
                timeSinceLastDifficultyInc = 0;
                spawnManager.IncreaseDifficulty(1);
                ShowDifficultyPopup(player.transform);
            }
        }
    }

    internal void ShowDifficultyPopup(Transform popupTransform)
    {
        GameObject difficultyPopup = Instantiate(difficultyPopupPrefab, popupTransform.position, new Quaternion());
        Destroy(difficultyPopup, difficultyPopupLength);
        difficultyPopup.transform.position = popupTransform.position;
    }

    internal void StartGame()
    {
        isInGame = true;
    }

    internal void StopGame()
    {
        isInGame = false;
    }
}
