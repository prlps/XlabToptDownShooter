using System;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Enemy[] m_enemies;
    [SerializeField] private EnemyData[] m_data;
    [SerializeField] private Transform[] m_spawnPoints;

    public void Spawn()
    {
        foreach (var spawnPoint in m_spawnPoints)
        {
            var prefab = GetEnemy();
            var data = GetEnemyData();

            var enemyInstance = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            enemyInstance.Initialize(data);

        
            enemyInstance.Died += OnDied;
        }
    }

    private void OnDied(Enemy enemy)
    {
      
    }

    private Enemy GetEnemy() =>
        m_enemies[UnityEngine.Random.Range(0, m_enemies.Length)];

    private EnemyData GetEnemyData() =>
        m_data[UnityEngine.Random.Range(0, m_data.Length)];
}
