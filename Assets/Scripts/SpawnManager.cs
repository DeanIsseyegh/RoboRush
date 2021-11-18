using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject obstaclePrefab;
    public GameObject enemyPrefab;
    public GameObject enemyDronePrefab;
    public GameObject flyingEnemyPrefab;
    public GameObject[] cloudPrefabs;
    
    private float startDelay = 0f;
    private float cloudSpawnInterval = 2f;
    private float enemySpawnRandomBuffer = 7f;
    private float cloudSpawnRandomBuffer = 2f;
    private float startSpawnPosX = 40f;
    private float startSpawnPosY = 6.5f;

    private int difficulty = 1;

    // Start is called before the first frame update
    public void StartSpawning()
    {
        StartCoroutine("SpawnEnemyRandomInterval", "SpawnFlyingEnemy");
        StartCoroutine("SpawnEnemyRandomInterval", "SpawnEnemy");
        StartCoroutine("SpawnEnemyRandomInterval", "SpawnEnemyDrone");
        StartCoroutine("SpawnEnemyRandomInterval", "SpawnObstacle");
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
            yield return new WaitForSeconds(Random.Range(1f, enemySpawnRandomBuffer / difficulty));
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
        Instantiate(obstaclePrefab, new Vector3(startSpawnPosX, 1f, 0), obstaclePrefab.transform.rotation);
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, new Vector3(startSpawnPosX, 0, 0), enemyPrefab.transform.rotation);
    }

    private void SpawnEnemyDrone()
    {
        Instantiate(enemyDronePrefab, new Vector3(startSpawnPosX, Random.Range(3, 8), 0), enemyPrefab.transform.rotation);
    }

    private void SpawnFlyingEnemy()
    {
        Instantiate(flyingEnemyPrefab, new Vector3(startSpawnPosX, Random.Range(startSpawnPosY, startSpawnPosY + 10), 0), enemyPrefab.transform.rotation);
    }    
    private void SpawnCloud()
    {
        var index = Random.Range(0, cloudPrefabs.Length);
        Instantiate(cloudPrefabs[index], new Vector3(600, Random.Range(15, 100), Random.Range(250, 350)), enemyPrefab.transform.rotation);
    }

}
