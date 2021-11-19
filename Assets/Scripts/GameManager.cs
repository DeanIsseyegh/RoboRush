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
    public GameObject difficultyManagerPopupObj;
    public GameObject playerObj;
    public GameObject groundObj;
    public GameObject initalCloudsObj;
    
    private PlayerController playerController;
    private SpawnManager spawnManager;
    private DifficultyManager difficultyPopupManager;
    private MoveLeft moveGroundLeft;
    private MoveLeft moveCloudsLeft;

    private UI ui;
    
    // Start is called before the first frame update
    void Start()
    {
        ui = GameObject.Find("GameUI").GetComponent<UI>();
        spawnManager = spawnManagerObj.GetComponent<SpawnManager>();
        playerController = playerObj.GetComponent<PlayerController>();
        moveGroundLeft = groundObj.GetComponent<MoveLeft>();
        moveCloudsLeft = initalCloudsObj.GetComponent<MoveLeft>();
        difficultyPopupManager = difficultyManagerPopupObj.GetComponent<DifficultyManager>();

        titleScreenUi.SetActive(true);
        inGameUi.SetActive(false);
        gameOverScreenUi.SetActive(false);
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
        difficultyPopupManager.StartGame();
    }

    public void EndGame()
    {
        DeathManager.KillAll();

        titleScreenUi.SetActive(false);
        inGameUi.SetActive(false);
        gameOverScreenUi.SetActive(true);
        ui.UpdateGameOverScore();

        moveGroundLeft.StopGame();
        moveCloudsLeft.StopGame();
        spawnManager.StopSpawning();
        playerController.StopGame();
        difficultyPopupManager.StopGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
