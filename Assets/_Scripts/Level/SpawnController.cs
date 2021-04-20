using System.Collections.Generic;
using UnityEngine;
using SpawnUtils;

public class SpawnController : MonoBehaviour
{
    [SerializeField]private GameObject enemyPrefab;
    [SerializeField] private float spawnDelay = 0.25f;
    [SerializeField] private float spawnInterval = 5.0f;
    private const string SPAWN_ENEMY_METHOD = "SpawnOneEnemy";
    private GameObject enemyParent;
    private IList<SpawnPoint> spawnPoints;
    private Stack<SpawnPoint> spawnStack;

    private void Start() 
    {
        enemyParent = GameObject.Find("EnemyParent");
        if(!enemyParent)
        {
            enemyParent = new GameObject("EnemyParent");
        }

        //get the spawn points
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
        SpawnEnemyWaves();
    }

    public void StopEnemyWaves()
    {
        // stop spawning enemys
        CancelInvoke();
    }

    private void SpawnEnemyWaves()
    {
        //create stack of points
        spawnStack = ListUtils.createShuffleStack(spawnPoints);
        InvokeRepeating(SPAWN_ENEMY_METHOD, spawnDelay, spawnInterval);
    }

    private void SpawnOneEnemy()
    {
        if(spawnStack.Count == 0)
        {
            spawnStack = ListUtils.createShuffleStack(spawnPoints);
        }
    
        var enemy = Instantiate(enemyPrefab, enemyParent.transform);
        var sp = spawnStack.Pop();
        enemy.transform.position = sp.transform.position;
    }

}
