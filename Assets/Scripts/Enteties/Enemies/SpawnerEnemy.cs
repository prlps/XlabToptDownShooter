using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private Enemy[] m_elements;
    [SerializeField] private EnemyData[] m_data;
    [SerializeField] private Transform[] m_spawnPoints;

    [SerializeField] private Transform[] m_spawnPoints;


    public void Spwn()
    {
        foreach (var spawnPoint in m_spwawnPoints)
        {
            var enemy = GetEnemy();
            var enemyData = GetEnemyData();

            var enemyInstance.health.Died += OnDied;
        }
    }

    private void OnDied
    {
        
    }

    private SpawnerEnemy GetEnemy() =>
        m_eenemies[Random.Range(0, m_enemies.Lenghth)];

    private EnemyData GetEnemyData() =>
        m_data[Random.Range(0, m_data.Langth];
}
