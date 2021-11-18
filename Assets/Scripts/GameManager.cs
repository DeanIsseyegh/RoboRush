using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject inGameUi;
    public GameObject titleScreenUi;
    public GameObject gameOverScreenUi;

    public GameObject spawnManagerObj;
    public GameObject playerObj;
    public GameObject groundObj;
    public GameObject initalCloudsObj;
    
    private PlayerController playerController;
    private SpawnManager spawnManager;
    private MoveLeft moveGroundLeft;
    private MoveLeft moveCloudsLeft;

    private UI ui;

    private float timeSinceLastDifficultyInc = 0f;
    private float difficultyIncreaseInterval = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("GameUI").GetComponent<UI>();
        spawnManager = spawnManagerObj.GetComponent<SpawnManager>();
        playerController = playerObj.GetComponent<PlayerController>();
        moveGroundLeft = groundObj.GetComponent<MoveLeft>();
        moveCloudsLeft = initalCloudsObj.GetComponent<MoveLeft>();

        titleScreenUi.SetActive(true);
        inGameUi.SetActive(false);
        gameOverScreenUi.SetActive(false);
    }

    private void Update()
    {
        if (timeSinceLastDifficultyInc > difficultyIncreaseInterval)
        {
            timeSinceLastDifficultyInc = 0;
            spawnManager.IncreaseDifficulty(1);
        }
    }

    public void BeginGame()
    {
        titleScreenUi.SetActive(false);
        inGameUi.SetActive(true);
        gameOverScreenUi.SetActive(false);

        moveGroundLeft.StartGame();
        moveCloudsLeft.StartGame();
        spawnManager.StartSpawning();
        playerController.StartGame();
    }

    public void EndGame()
    {
        DestroyAll(GameObject.FindGameObjectsWithTag("Enemy"));
        DestroyAll(GameObject.FindGameObjectsWithTag("Obstacle"));

        titleScreenUi.SetActive(false);
        inGameUi.SetActive(false);
        gameOverScreenUi.SetActive(true);
        ui.UpdateGameOverScore();

        moveGroundLeft.StopGame();
        moveCloudsLeft.StopGame();
        spawnManager.StopSpawning();
        playerController.StopGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private static void DestroyAll(GameObject[] gameObjects)
    {
        foreach (GameObject toDestroy in gameObjects)
        {
            Destroy(toDestroy);
        }
    }

}
