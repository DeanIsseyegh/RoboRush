using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject obstaclePrefab;
    public GameObject enemyPrefab;
    public GameObject enemyDronePrefab;
    public GameObject flyingEnemyPrefab;
    public GameObject[] powerupPrefabs;
    public GameObject[] cloudPrefabs;
    
    private float startDelay = 0f;
    private float cloudSpawnInterval = 2f;
    private float cloudSpawnRandomBuffer = 2f;

    private System.Func<int, float> EnemySpawnInterval = (difficulty) => Random.Range(2f, 9f - difficulty);

    private System.Func<float> EnemyDroneSpawnPosY = () => Random.Range(3f, 8f);
    private System.Func<float> FlyingEnemySpawnPosY = () => Random.Range(6.5f, 16.5f);

    private System.Func<float> PowerUpSpawnInterval = () => Random.Range(5f, 15f);
    private System.Func<float> PowerupSpawnPosY = () => Random.Range(1f, 7f);

    private float spawnPosX = 40f;

    private float cloudSpawnPosX = 600f;
    private System.Func<float> CloudSpawnPosY = () => Random.Range(15, 100);
    private System.Func<float> CloudSpawnPosZ = () => Random.Range(250, 350);

    private int difficulty = 1;

    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemyRandomInterval", "SpawnFlyingEnemy");
        StartCoroutine("SpawnEnemyRandomInterval", "SpawnEnemy");
        StartCoroutine("SpawnEnemyRandomInterval", "SpawnEnemyDrone");
        StartCoroutine("SpawnEnemyRandomInterval", "SpawnObstacle");
        StartCoroutine("SpawnPowerupRandomInterval", "SpawnPowerup");
        InvokeRepeating("SpawnCloudRandomInterval", startDelay, cloudSpawnInterval);
    }
    internal void StopSpawning()
    {
        CancelInvoke();
        Destroy(gameObject);
    }

    internal void IncreaseDifficulty(int amount)
    {
        difficulty += amount;
    }

    private IEnumerator SpawnEnemyRandomInterval(string spawnMethod)
    {
        while (true)
        {
            yield return new WaitForSeconds(EnemySpawnInterval(difficulty));
            Invoke(spawnMethod, 0f);
        }
    }

    private IEnumerator SpawnPowerupRandomInterval(string spawnMethod)
    {
        while (true)
        {
            yield return new WaitForSeconds(PowerUpSpawnInterval());
            Invoke(spawnMethod, 0f);
        }
    }

    private void SpawnCloudRandomInterval()
    {
        float randomDelay = Random.Range(0f, cloudSpawnRandomBuffer);
        Invoke("SpawnCloud", randomDelay);
    }

    private void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, new Vector3(spawnPosX, 1f, 0), obstaclePrefab.transform.rotation);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(spawnPosX, 0, 0), enemyPrefab.transform.rotation);
    }

    private void SpawnEnemyDrone()
    {
        Instantiate(enemyDronePrefab, new Vector3(spawnPosX, EnemyDroneSpawnPosY(), 0), enemyPrefab.transform.rotation);
    }

    private void SpawnFlyingEnemy()
    {
        Instantiate(flyingEnemyPrefab, new Vector3(spawnPosX, FlyingEnemySpawnPosY(), 0), enemyPrefab.transform.rotation);
    }    
    private void SpawnCloud()
    {
        var index = Random.Range(0, cloudPrefabs.Length);
        Instantiate(cloudPrefabs[index], new Vector3(cloudSpawnPosX, CloudSpawnPosY(), CloudSpawnPosZ()), enemyPrefab.transform.rotation);
    }

    private void SpawnPowerup()
    {
        var index = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[index], new Vector3(spawnPosX, PowerupSpawnPosY(), 0), powerupPrefabs[index].transform.rotation);
    }

}
