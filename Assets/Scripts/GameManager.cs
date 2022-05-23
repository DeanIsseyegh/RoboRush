using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SerializedMonoBehaviour
{
    public GameObject inGameUi;
    public GameObject titleScreenUi;
    public GameObject gameOverScreenUi;
    public GameObject pauseScreenUi;

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

    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject unPauseButton;
    [SerializeField] private ControlsManager controlsManager;
    private IPlayerControlsInput _playerControlsInput;


    [SerializeField] private int initialControlDisableMs = 500;
    
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
        _playerControlsInput = controlsManager.GetControls();
        _playerControlsInput.DisableInGameActions();
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
        Task.Delay(initialControlDisableMs).ContinueWith(t=> _playerControlsInput.EnableInGameActions());
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

    public void PauseGame()
    {
        Time.timeScale = 0;
        foreach (var audioSource in audioSources)
        {
            audioSource.Pause();
        }
        pauseButton.SetActive(false);
        unPauseButton.SetActive(true);
        _playerControlsInput.DisableInGameActions();
        pauseScreenUi.SetActive(true);
    }
    
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        foreach (var audioSource in audioSources)
        {
            audioSource.Play();
        }
        pauseButton.SetActive(true);
        unPauseButton.SetActive(false);
        _playerControlsInput.EnableInGameActions();
        pauseScreenUi.SetActive(false);
    }

}
