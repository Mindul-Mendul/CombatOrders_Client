using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController: MonoBehaviour
{
    public float spawnDelay=3f;

    SpawnPoint[] spawnPoints;
    Dictionary<SpawnPoint, bool> spawnPointStates = new Dictionary<SpawnPoint, bool>();

    private void Awake()
    {
        spawnPoints=new SpawnPoint[transform.childCount];
        for (int i= 0; i< transform.childCount; i++)
        {
            spawnPoints[i]=transform.GetChild(i).GetComponent<SpawnPoint>();
            spawnPointStates[spawnPoints[i]] = true;
        }

        InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay);
    }

    private void SpawnEnemy()
    {
        foreach (SpawnPoint spawnPoint in spawnPoints)
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

    private void OnEnemyDeath(SpawnPoint spawnPoint)
    {
        // 해당 스폰 포인트에서 생성된 몹이 죽으면 상태를 true로 변경하여 다시 몹 생성 가능하도록 함
        spawnPointStates[spawnPoint] = true;
    }
}
