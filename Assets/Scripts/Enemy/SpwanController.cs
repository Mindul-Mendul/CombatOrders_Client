using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanController: MonoBehaviour
{
    public SpwanPoint[] spawnPoints;
    public float spawnDelay;

    private Dictionary<SpwanPoint, bool> spawnPointStates = new Dictionary<SpwanPoint, bool>();

    private void Awake()
    {
        foreach (SpwanPoint spawnPoint in spawnPoints)
        {
            spawnPointStates[spawnPoint] = true;
        }

        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay);
    }

    private void SpawnEnemy()
    {
        foreach (SpwanPoint spawnPoint in spawnPoints)
        {
            if (spawnPointStates[spawnPoint])
            {
                GameObject enemy = Instantiate(spawnPoint.enemyPrefab, spawnPoint.GetComponent<Transform>().position, Quaternion.identity);
                enemy.GetComponent<Enemy>().OnDeath += () => OnEnemyDeath(spawnPoint);
                enemy.transform.parent = spawnPoint.transform;
                spawnPointStates[spawnPoint] = false;
            }
        }
    }

    private void OnEnemyDeath(SpwanPoint spawnPoint)
    {
        // 해당 스폰 포인트에서 생성된 몹이 죽으면 상태를 true로 변경하여 다시 몹 생성 가능하도록 함
        spawnPointStates[spawnPoint] = true;
    }
}
