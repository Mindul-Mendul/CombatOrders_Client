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
        // �ش� ���� ����Ʈ���� ������ ���� ������ ���¸� true�� �����Ͽ� �ٽ� �� ���� �����ϵ��� ��
        spawnPointStates[spawnPoint] = true;
    }
}
