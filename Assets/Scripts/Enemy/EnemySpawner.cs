using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;

    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 3;
    public int totalKill = 0;
    private int totalKillWave = 0;

    [SerializeField] private float spawnInterval = 3f;

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 0;
    public int defaultSpawnCount = 1;
    public int spawnCountMultiplier = 1;
    public int multiplierIncreaseCount = 1;

    public CombatManager combatManager;

    public bool isSpawning = false;

    private void Start()
    {
        spawnCount = defaultSpawnCount;
    }

    public void StartSpawning()
    {
        if (!isSpawning && spawnedEnemy.level <= combatManager.waveNumber)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemies());
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemies()
    {
        while (isSpawning)
        {
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        if (spawnedEnemy != null)
        {
            Enemy enemyInstance = Instantiate(spawnedEnemy);
            enemyInstance.GetComponent<Enemy>().combatmanager = combatManager;
            enemyInstance.GetComponent<Enemy>().spawner = this;
            combatManager.totalEnemies++;
        }
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;

        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            spawnCount = defaultSpawnCount + (spawnCountMultiplier * multiplierIncreaseCount);
            multiplierIncreaseCount++;
        }
    }
}
