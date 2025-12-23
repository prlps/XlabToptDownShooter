using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO.Enumeration;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] 
    [SerializeField] private HealthComponent m_health;
    private EnemyData m_data;

    public IHealth health => m_health;

    public event Action<Enemy> Died;

    //TODO add HealtgComponent
    //TODO add Movment
    //TODO Add AttackComponent

    private void Awake() {
        Initialize(m_enemyData);
    }

    private void OnEnable() 
    {
        m_health.ValueChanged += () =>
        {
            Debug.Log("Health Changed {m_healt.Value}");
        };
    }

    private void OnDisable()
    {
        m_health.Died -= OnDied;
    }
    public void Initialize(EnemyData data)
    {
        m_data = data;
        m_health.Initialize(data.health);
    }
    private void OnDied()
    {
        Debug.Log("Enemy Died");
        Destroy(gameObject);
    }

    private void OnDied() =>
        Died?.Invoke(this);

}