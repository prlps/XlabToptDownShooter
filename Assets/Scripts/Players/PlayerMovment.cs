using System;
using Enteties;
using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour, IAcceleration
    {
        public event Action Stopped;
        public event Action<Vector3> DestinationChanged;

        [SerializeField] private NavMeshAgent m_agent;

        private float m_speed;
        private float m_acceleration;
        private float m_angularSpeed;
        private bool m_hasDestination;

        private void OnValidate()
        {
            if (m_agent == null)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void Awake()
        {
            if (m_agent == null)
            {
                m_agent = GetComponent<NavMeshAgent>();
            }
        }

        private void Update()
        {
            if (!m_hasDestination || m_agent == null || m_agent.pathPending)
            {
                return;
            }

            if (m_agent.remainingDistance <= m_agent.stoppingDistance)
            {
                if (!m_agent.hasPath || m_agent.velocity.sqrMagnitude <= 0.001f)
                {
                    m_hasDestination = false;
                    Stopped?.Invoke();
                }
            }
        }

        public void Initialize(float speed, float angularSpeed)
        {
            m_speed = speed;
            m_angularSpeed = angularSpeed;

            if (m_agent == null)
            {
                return;
            }

            m_agent.speed = speed;
            m_agent.angularSpeed = angularSpeed;
            m_agent.updateRotation = false;
        }

        public void IncreaseAcceleration(float delta)
        {
            if (delta < 0f)
            {
                throw new ArgumentException("Delta can not be negative", nameof(delta));
            }

            m_acceleration += delta;
            SetSpeed();
        }

        public void DecreaseAcceleration(float delta)
        {
            if (delta < 0f)
            {
                throw new ArgumentException("Delta can not be negative", nameof(delta));
            }

            m_acceleration -= delta;
            SetSpeed();
        }

        public void SetDestination(Vector3 navMeshPoint)
        {
            if (m_agent == null)
            {
                return;
            }

            m_agent.SetDestination(navMeshPoint);
            m_hasDestination = true;

            DestinationChanged?.Invoke(navMeshPoint);
        }

        public void RotateTowards(Vector3 worldPoint)
        {
            var direction = worldPoint - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude < 0.0001f)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            var angularSpeed = m_agent != null ? m_agent.angularSpeed : m_angularSpeed;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, angularSpeed * Time.deltaTime);
        }

        private void SetSpeed()
        {
            if (m_agent == null)
            {
                return;
            }

            var acceleration = m_acceleration > 0f ? m_acceleration : 1f;
            m_agent.speed = m_speed * acceleration;
        }
    }
}
