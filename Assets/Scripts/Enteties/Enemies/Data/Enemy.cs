using System;
using Enteties.Enemies;
using Enteties.Enemies.Systems;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> Died;

    [SerializeField] private HealthComponent m_health;
    [SerializeField] private AttackEnemySystem m_attack;
    [SerializeField] private EnemyMovment m_movment;

    private EnemyData m_data;
    private Transform m_playerTransform;
    private EnemyStateMachine m_stateMachine;

    private void Awake()
    {
        m_stateMachine = new EnemyStateMachine();
        EnsureComponents();
    }

    private void OnEnable()
    {
        if (m_health != null)
        {
            m_health.died += OnDied;
        }

        m_stateMachine.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        if (m_health != null)
        {
            m_health.died -= OnDied;
        }

        m_stateMachine.StateChanged -= OnStateChanged;
    }

    private void Update()
    {
        if (m_stateMachine.CurrentState is EnemyState.Dead || m_data == null)
        {
            return;
        }

        UpdateState();
    }

    public void Initialize(EnemyData data, Transform playerTransform)
    {
        m_data = data;
        m_playerTransform = playerTransform;

        EnsureComponents();

        m_health.Initialize(data.health);
        m_attack.Initialize(data.defaultSpell, data.spells, playerTransform, data.attackTime);
        m_movment.Initialize(data.speed, playerTransform);

        if (data.enemyType == AttackEnemyType.Melee)
        {
            m_stateMachine.ChangeState(EnemyState.Move);
        }
    }

    private void EnsureComponents()
    {
        if (m_health == null)
        {
            m_health = GetComponent<HealthComponent>() ?? gameObject.AddComponent<HealthComponent>();
        }

        if (m_attack == null)
        {
            m_attack = GetComponent<AttackEnemySystem>() ?? gameObject.AddComponent<AttackEnemySystem>();
        }

        if (m_movment == null)
        {
            m_movment = GetComponent<EnemyMovment>() ?? gameObject.AddComponent<EnemyMovment>();
        }
    }

    private void UpdateState()
    {
        var isInAttackRange = IsInRange();

        switch (m_stateMachine.CurrentState)
        {
            case EnemyState.Idle:
                HandleIdleState(isInAttackRange);
                break;
            case EnemyState.Move:
                HandleMoveState(isInAttackRange);
                break;
            case EnemyState.Attack:
                HandleAttackState(isInAttackRange);
                break;
        }
    }

    private void HandleAttackState(bool isInAttackRange)
    {
        m_attack.TryAttack();

        if (!isInAttackRange)
        {
            if (m_data.enemyType == AttackEnemyType.Melee)
            {
                m_stateMachine.ChangeState(EnemyState.Move);
            }
            else
            {
                m_stateMachine.ChangeState(EnemyState.Idle);
            }
        }
    }

    private void HandleMoveState(bool isInAttackRange)
    {
        if (isInAttackRange)
        {
            m_stateMachine.ChangeState(EnemyState.Attack);
        }
    }

    private void HandleIdleState(bool isInAttackRange)
    {
        if (m_data.enemyType == AttackEnemyType.Range && isInAttackRange)
        {
            m_stateMachine.ChangeState(EnemyState.Attack);
        }
    }

    private bool IsInRange()
    {
        if (!m_playerTransform)
        {
            return false;
        }

        var distance = Vector3.Distance(transform.position, m_playerTransform.position);
        return distance < m_data.attackRange;
    }

    private void OnDied()
    {
        m_stateMachine.ChangeState(EnemyState.Dead);
        Died?.Invoke(this);
        Destroy(gameObject);
    }

    private void OnStateChanged(EnemyState previousState, EnemyState nextState)
    {
        if (previousState is EnemyState.Move)
        {
            m_movment.StopMoving();
        }

        if (nextState is EnemyState.Move)
        {
            m_movment.StartMoving();
        }
    }
}
