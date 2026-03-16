using System.Collections.Generic;
using Infrastucture;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Enemy[] m_enemies;
    [SerializeField] private EnemyData[] m_data;
    [SerializeField] private Transform[] m_spawnPoints;

    private readonly List<Enemy> m_currentEnemies = new();

    public void Spawn()
    {
        var playerTransform = ServiceLocator
            .Resolved<IPlayerFactory>()
            .Create()
            .transform;

        foreach (var spawnPoint in m_spawnPoints)
        {
            var prefab = GetEnemy();
            var data = GetEnemyData();

            var enemyInstance = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
            enemyInstance.Initialize(data, playerTransform);

            enemyInstance.Died += OnDied;
            m_currentEnemies.Add(enemyInstance);
        }
    }

    public void DespawnEnemyAll()
    {
        foreach (var enemy in m_currentEnemies)
        {
            DestroyEnemy(enemy);
        }

        m_currentEnemies.Clear();
    }

    private void OnDied(Enemy enemy)
    {
        m_currentEnemies.Remove(enemy);
        DestroyEnemy(enemy);
    }

    private Enemy GetEnemy() =>
        m_enemies[Random.Range(0, m_enemies.Length)];

    private EnemyData GetEnemyData() =>
        m_data[Random.Range(0, m_data.Length)];

    private void DestroyEnemy(Enemy enemy)
    {
        if (enemy == null)
        {
            return;
        }

        enemy.Died -= OnDied;
        Destroy(enemy.gameObject);
    }
}
