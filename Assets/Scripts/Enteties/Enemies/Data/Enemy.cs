using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthComponent m_health;
    private EnemyData m_data;

    public HealthComponent health => m_health;

    public event Action<Enemy> Died;

    //TODO add HealtgComponent
    //TODO add Movment
    //TODO Add AttackComponent

    private void Awake()
    {
  
    }

    private void OnEnable()
    {
        if (m_health != null)
        {
            m_health.ValueChanged += OnValueChanged;
            m_health.Died += OnDiedInternal;
        }
    }

    private void OnDisable()
    {
        if (m_health != null)
        {
            m_health.ValueChanged -= OnValueChanged;
            m_health.Died -= OnDiedInternal;
        }
    }

    private void OnValueChanged()
    {
        Debug.Log($"Health Changed {m_health.Value}");
    }

    public void Initialize(EnemyData data)
    {
        m_data = data;
        if (m_health != null)
            m_health.Initialize(data.healt);
    }

    private void OnDiedInternal()
    {
        Debug.Log("Enemy Died");
        Died?.Invoke(this);
        Destroy(gameObject);
    }
}