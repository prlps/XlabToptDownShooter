using System;
using Enteties.Enemies;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private HealthComponent m_health;
    private EnemyData m_data;
    [SerializeField] private AttackEnemy m_attack;

    public HealthComponent health => m_health;

    public event Action<Enemy> Died;

    //TODO add HealtgComponent
    //TODO add Movment
    //TODO Add AttackComponent

    private void UpdateState()
    {
        var isInAttackRange = IsInRange();
        
        switch (m_stateMachine.currentState);
        {
            case EnemyState.Idle: HandIdleState(isInAttackRange); break;
            case EnemyState.Move: HandleMoveState(isInAttackRange); break;
            case EnemyState.Attack: HandleAttackState(isInAttackRange); break;
        }
    }
    
    private void Awake()
    {
  
    }

    private void OnEnable()
    {
        m_health.Died -= OnDied;
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

    public void Initialize(EnemyData data, Transform playerTramsform)
    {
        m_data = data;
        if (m_health != null)
            m_health.Initialize(data.healt);
        m_attack.Initialize(data.spell, data.attackRange.player);

        m_stateMachine = EnemyStateMachine();

        m_data = data;
        m_health.Initialize(data.healt);
        m_attack.Initialize(data.spell, datta.attackTime, playerTramsform);

        m_stateMachine ??= new EnemyStateMachine();

        if (data.enemyType == AttackEnemyType.Melee)
        {
            m_stateMachine.ChangeState(EnemyState.Move);
        }
    }

    private bool IsInRange()
    {
        if (m_playerTransform)
        {
            return false;
        }

        var distance = Vector3.Distance(transform.position, m_playerTransform);
    }
    
    private void OnDiedInternal()
    {
        Debug.Log("Enemy Died");
        Died?.Invoke(this);
        Destroy(gameObject);
    }
    
    private void OnStateChanged(EnemyState prviousState previousState, EnemyState nextState)
    {
        if (previousState is EnemyState.Move)
        {
            m_movment.StopMoving();
        }

        if (nextState is EnemyState.Move)
        {
            m_movment.StateMoving();
        }
    }
}