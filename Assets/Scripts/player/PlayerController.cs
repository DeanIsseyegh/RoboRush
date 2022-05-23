using System;
using System.Collections;
using player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : SerializedMonoBehaviour
{

    public GameObject laser;
    public GameObject missle;
    public GameObject barrelExplosion;
    public GameObject invincibleIndicator;
    public ParticleSystem dirtSplatter;
    private GameManager gameManager;

    [SerializeField] private AudioSource playerSoundEffectsSource;
    [SerializeField] private AudioSource wheelsAudioSource;
    [SerializeField] private AudioClip playerHurtSound;
    [SerializeField] private AudioClip playerLaserShootSound;
    [SerializeField] private AudioClip playerMissileShootSound;
    [SerializeField] private AudioClip playerJumpSound;
    [SerializeField] private AudioClip playerDeathSound;
    
    [SerializeField] private ControlsManager controlsManager;
    private IPlayerControlsInput _playerControlsInput;

    private Animator animator;

    private bool isInGame = false;

    private UI ui;

    private Rigidbody playerRb;

    private float speed = 10;
    private float jumpForce = 1000;
    [SerializeField] private int allowedJumps = 2;
    [FormerlySerializedAs("doubleJumpCooldown")] [SerializeField] private float jumpCooldown = 0.2f;
    private float timeSinceJump;
    private Vector3 gravity = new Vector3(0, -29.4f, 0);
    private int jumpCounter = 0;
    [SerializeField] private float leftXBoundary = 5.2f;
    [SerializeField] private float rightXBoundary = 24.7f;
    private float yBoundary = 0.5f;

    private float laserAttackLength = 0.25f;
    private float timeSinceLastLaserAttack = 999;
    private float laserAttackReloadTime = 2.5f;

    private float timeSinceLastMissileAttack = 999;
    private float missileAttackReloadTime = 1.25f;

    private bool isInvincible = false;
    private bool isInvincibleEndingTriggered = false;
    private float invincibleTimeLeft = 0f;
    private Coroutine invincibleEndingCo;

    private SkinnedMeshRenderer skinMeshRenderers;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GameObject.Find("PlayerModel").GetComponent<Animator>();
        ui = GameObject.Find("GameUI").GetComponent<UI>();
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity = gravity;
        dirtSplatter.Stop();
        skinMeshRenderers = GetComponentInChildren<SkinnedMeshRenderer>();
        _playerControlsInput = controlsManager.GetControls();
    }

    public void StartGame()
    {
        isInGame = true;
        animator.SetTrigger("Start Game");
        wheelsAudioSource.Play();
    }

    void Update()
    {
        if (isInGame)
        {
            UpdateInvincibility();
            ControlPlayerJumping();
            ControlPlayerAttack();
            ControlPlayerXMovement();
            ControlDirtSplatter();
            ApplyBoundaries();
            UpdateScoreOverTime();
            CheckForGameOver();
        }
    }

    internal void UpdateInvincibility()
    {
        if (isInvincible)
        {
            invincibleTimeLeft -= Time.deltaTime;
            if (!isInvincibleEndingTriggered)
            {
                invincibleIndicator.SetActive(true);
                if (invincibleTimeLeft < 1f)
                {
                    isInvincibleEndingTriggered = true;
                    invincibleEndingCo = StartCoroutine(StartInvincibilityEnding());
                }
            }
        }
    }

    internal void MakeInvincible(float invincibleTime)
    {
        if (invincibleEndingCo != null)
        {
            StopCoroutine(invincibleEndingCo);
            isInvincibleEndingTriggered = false;
        }
        isInvincible = true;
        invincibleTimeLeft = invincibleTime;
    }

    private void ControlDirtSplatter()
    {
        if ((jumpCounter == 1 || IsDead()) && dirtSplatter.isPlaying)
        {
            dirtSplatter.Stop();
        }
        else if (jumpCounter == 0 && !dirtSplatter.isPlaying)
        {
            dirtSplatter.Play();
        }
    }

    public void StopGame()
    {
        playerSoundEffectsSource.PlayOneShot(playerDeathSound);
        isInGame = false;
        animator.SetTrigger("End Game");
        wheelsAudioSource.Stop();
    }

    public void PauseGame()
    {
        isInGame = false;
    }
    
    public void UnPauseGame()
    {
        isInGame = true;
    }

    private void CheckForGameOver()
    {
        if (IsDead()) gameManager.EndGame();
    }

    private bool IsDead()
    {
        return ui.GetHealth() <= 0;
    }

    private void UpdateScoreOverTime()
    {
        ui.UpdateScore(Time.deltaTime * 10);
    }

    private void ControlPlayerJumping()
    {
        timeSinceJump += Time.deltaTime;
        if (jumpCooldown > timeSinceJump) return;
        if (jumpCounter < allowedJumps && _playerControlsInput.IsJumpPressed())
        {
            if (jumpCounter == 1) {
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, 0); //reset air velocity before double jump 
            }
            jumpCounter++;
            timeSinceJump = 0;
            playerSoundEffectsSource.PlayOneShot(playerJumpSound);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void ControlPlayerAttack()
    {
        timeSinceLastLaserAttack += Time.deltaTime;
        LaserBar.instance.RefillLaserAmmo(timeSinceLastLaserAttack / laserAttackReloadTime);
        if (_playerControlsInput.IsPrimaryAttackPressed() && timeSinceLastLaserAttack > laserAttackReloadTime)
        {
            StartCoroutine(StartPlayerLaserAttack());
        }

        timeSinceLastMissileAttack += Time.deltaTime;
        MissileBar.instance.RefillAmmo(timeSinceLastMissileAttack / missileAttackReloadTime);
        if (_playerControlsInput.IsSecondaryAttackPressed() && timeSinceLastMissileAttack > missileAttackReloadTime)
        {
            StartPlayerMissileAttack();
        }
    }

    private void ControlPlayerXMovement()
    {
        if (_playerControlsInput.IsLeftPressed())
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (_playerControlsInput.IsRightPressed())
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    private void ApplyBoundaries()
    {
        if (transform.position.x < leftXBoundary)
        {
            transform.position = changeOnlyX(transform.position, leftXBoundary);
        }
        else if (transform.position.x > rightXBoundary)
        {
            transform.position = changeOnlyX(transform.position, rightXBoundary);
        }

        if (transform.position.y < yBoundary)
        {
            transform.position = changeOnlyY(transform.position, yBoundary);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCounter = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle") || other.gameObject.CompareTag("Enemy"))
        {
            DeathManager deathManager = other.gameObject.GetComponent<DeathManager>();
            if (isInvincible)
            {
                deathManager.Kill(0, 50);
            }
            else
            {
                StartCoroutine("IndicateHurt");
                playerSoundEffectsSource.PlayOneShot(playerHurtSound);
                deathManager.Kill(-1, -50);
            }
        }
    }

    private Vector3 changeOnlyX(Vector3 vector3, float x)
    {
        return new Vector3(x, vector3.y, vector3.z);
    }

    private Vector3 changeOnlyY(Vector3 vector3, float y)
    {
        return new Vector3(vector3.x, y, vector3.z);
    }

    private void StartPlayerMissileAttack()
    {
        timeSinceLastMissileAttack = 0;
        GameObject missileShot = Instantiate(missle);
        playerSoundEffectsSource.PlayOneShot(playerMissileShootSound);
        MissileBar.instance.UseAmmo();
    }


    private IEnumerator StartPlayerLaserAttack()
    {
        timeSinceLastLaserAttack = 0;
        GameObject laserShot = Instantiate(laser);
        playerSoundEffectsSource.PlayOneShot(playerLaserShootSound);
        LaserBar.instance.UseLaserAmmo();
        yield return new WaitForSeconds(laserAttackLength);
        Destroy(laserShot);
    }

    private IEnumerator IndicateHurt()
    {
        float flashTime = .025f;
        float noOfFlashes = 10;
        for (int i = 0; i < noOfFlashes; i++)
        {
            yield return new WaitForSeconds(flashTime);
            skinMeshRenderers.enabled = false;
            yield return new WaitForSeconds(flashTime);
            skinMeshRenderers.enabled = true;
        }
    }

    private IEnumerator StartInvincibilityEnding()
    {
        float flashTime = 0.1f;
        float noOfFlashes = 10;
        for (int i = 0; i < noOfFlashes; i++)
        {
            yield return new WaitForSeconds(flashTime);
            invincibleIndicator.SetActive(false);
            yield return new WaitForSeconds(flashTime);
            invincibleIndicator.SetActive(true);
        }
        isInvincible = false;
        isInvincibleEndingTriggered = false;
        invincibleIndicator.SetActive(false);
        invincibleTimeLeft = 0;
    }
}
