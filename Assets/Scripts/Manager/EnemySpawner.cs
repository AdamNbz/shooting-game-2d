using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject turretPrefab;
    [SerializeField] private int enemyCount = 0;
    [SerializeField] private int turretCount = 0;
    private float spawnInterval = 2f;
    private float spawnTimer = 0f;
    private int enemyLimit = 5;
    private int turretLimit = 2;

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                if (enemyCount < enemyLimit) 
                {
                    SpawnEnemy();
                    enemyCount++;
                }
                if (turretCount < turretLimit)
                {
                    SpawnTurret();
                    turretCount++;
                }
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
        Vector2 cameraBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        float x = UnityEngine.Random.Range(-cameraBound.x, cameraBound.x);
        float y = UnityEngine.Random.Range(-cameraBound.y, cameraBound.y);
        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    private void SpawnTurret()
    {
        Vector2 cameraBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        
        float x = UnityEngine.Random.Range(-cameraBound.x, cameraBound.x);
        float y = UnityEngine.Random.Range(-cameraBound.y, cameraBound.y);
        Vector2 spawnPos = new Vector2(x, y);

        Instantiate(turretPrefab, spawnPos, Quaternion.identity);
    }

    public void OnEnemyKilled()
    {
        GameManager.instance.score += 10;
        enemyCount--;
    }
    public void OnTurretDestroyed()
    {
        GameManager.instance.score += 20;
        turretCount--;
    }
}
