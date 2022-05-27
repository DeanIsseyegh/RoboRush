using System.Collections;
using System.Collections.Generic;
using objectpooling;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [field: SerializeField] public SimpleObjectPool ObstaclePool { get; private set; }
    [SerializeField] private SimpleObjectPool enemyPool;
    [SerializeField] private SimpleObjectPool enemyDronePool;
    [SerializeField] private SimpleObjectPool flyingEnemyPool;
    [SerializeField] private SimpleObjectPool[] powerupPools;
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
        // InvokeRepeating("SpawnCloudRandomInterval", startDelay, cloudSpawnInterval);
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
        GameObject obstaclePrefab = ObstaclePool.Get();
        obstaclePrefab.transform.position = new Vector3(spawnPosX, 1f, 0);
        obstaclePrefab.transform.rotation = obstaclePrefab.transform.rotation;
    }

    private void SpawnEnemy()
    {
        GameObject enemyPrefab = enemyPool.Get();
        enemyPrefab.transform.position = new Vector3(spawnPosX, 0, 0);
        enemyPrefab.transform.rotation = enemyPrefab.transform.rotation;
    }

    private void SpawnEnemyDrone()
    {
        GameObject enemyDronePrefab = enemyDronePool.Get();
        enemyDronePrefab.transform.position = new Vector3(spawnPosX, EnemyDroneSpawnPosY(), 0);
        enemyDronePrefab.transform.rotation = enemyDronePrefab.transform.rotation;
    }

    private void SpawnFlyingEnemy()
    {
        GameObject flyingEnemyPrefab = flyingEnemyPool.Get();
        flyingEnemyPrefab.transform.position = new Vector3(spawnPosX, FlyingEnemySpawnPosY(), 0);
        flyingEnemyPrefab.transform.rotation = flyingEnemyPrefab.transform.rotation;
    }    
    private void SpawnCloud()
    {
        var index = Random.Range(0, cloudPrefabs.Length);
        Instantiate(cloudPrefabs[index], new Vector3(cloudSpawnPosX, CloudSpawnPosY(), CloudSpawnPosZ()), cloudPrefabs[index].transform.rotation);
    }

    private void SpawnPowerup()
    {
        var index = Random.Range(0, powerupPools.Length);
        SimpleObjectPool powerupPool = powerupPools[index];
        GameObject powerupPrefab = powerupPool.Get();
        powerupPrefab.transform.position = new Vector3(spawnPosX, PowerupSpawnPosY(), 0);
    }

}
