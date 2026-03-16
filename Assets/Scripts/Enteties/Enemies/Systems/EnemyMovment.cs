using System;
using Enteties;
using UnityEngine;
using UnityEngine.AI;

namespace Enteties.Enemies.Systems
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMovment : MonoBehaviour, IAcceleration
    {
        [SerializeField] private NavMeshAgent m_agent;

        private const float NavMeshSnapDistance = 8f;
        private Transform m_target;
        private float m_speed;
        private float m_acceleration;
        private bool m_isMoving;
        private bool m_isInitialized;

        private void OnValidate()
        {
            if (!m_agent)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void Awake()
        {
            if (!m_agent)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }
        }

        public void Initialize(float speed, Transform target)
        {
            m_speed = speed;
            m_target = target;
            m_isInitialized = true;

            if (!m_agent)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }

            if (m_agent != null)
            {
                m_agent.speed = speed;
                TrySnapToNavMesh();
            }
        }

        public void IncreaseAcceleration(float delta)
        {
            if (delta < 0)
            {
                throw new ArgumentException("Delta cannot be negative", nameof(delta));
            }

            m_acceleration += delta;
            SetSpeed();
        }

        public void DecreaseAcceleration(float delta)
        {
            if (delta < 0)
            {
                throw new ArgumentException("Delta cannot be negative", nameof(delta));
            }

            m_acceleration -= delta;
            SetSpeed();
        }

        private void Update()
        {
            if (!m_isInitialized || !m_isMoving || !m_target || !TrySnapToNavMesh())
            {
                return;
            }

            m_agent.SetDestination(m_target.position);
        }

        public void StartMoving()
        {
            if (!m_isInitialized)
            {
                return;
            }

            m_isMoving = true;
            if (!TrySnapToNavMesh())
            {
                return;
            }

            m_agent.isStopped = false;
        }

        public void StopMoving()
        {
            if (!m_isInitialized || !m_agent)
            {
                return;
            }

            m_isMoving = false;
            if (!m_agent.isActiveAndEnabled || !m_agent.isOnNavMesh)
            {
                return;
            }

            m_agent.isStopped = true;
            m_agent.velocity = Vector3.zero;
        }

        private void SetSpeed()
        {
            if (!m_agent)
            {
                return;
            }

            var acceleration = m_acceleration > 0
                ? m_acceleration
                : 1f;

            m_agent.speed = m_speed * acceleration;
        }

        private bool TrySnapToNavMesh()
        {
            if (!m_agent || !m_agent.isActiveAndEnabled)
            {
                return false;
            }

            if (m_agent.isOnNavMesh)
            {
                return true;
            }

            if (NavMesh.SamplePosition(transform.position, out var hit, NavMeshSnapDistance, NavMesh.AllAreas))
            {
                return m_agent.Warp(hit.position);
            }

            return false;
        }
    }
}
