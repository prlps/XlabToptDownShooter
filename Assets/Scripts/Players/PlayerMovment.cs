using System;
using UnityEngine;
using UnityEngine.AI;

namespace Players
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        public event Action Stopped;
        public event Action<Vector3> DestinationChanged;

        [SerializeField] private NavMeshAgent m_agent;

        private float m_speed;
        private float m_angularSpeed;
        private bool m_hasDestination;

        private void OnValidate()
        {
            if (m_agent == null) m_agent = GetComponent<NavMeshAgent>();
        }

        private void Awake()
        {
            if (m_agent == null) m_agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            if (!m_hasDestination || m_agent == null || m_agent.pathPending) return;

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
            if (m_agent != null)
            {
                m_agent.speed = speed;
                m_agent.angularSpeed = angularSpeed;
                m_agent.updateRotation = false;
            }
        }

        public void SetDestination(Vector3 navMeshPoint)
        {
            if (m_agent == null) return;
            m_agent.SetDestination(navMeshPoint);
            m_hasDestination = true;
            DestinationChanged?.Invoke(navMeshPoint);
        }

        public void RotateTowards(Vector3 worldPoint)
        {
            var dir = worldPoint - transform.position;
            dir.y = 0f;
            if (dir.sqrMagnitude < 0.0001f) return;
            var targetRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_agent != null ? m_agent.angularSpeed * Time.deltaTime : m_angularSpeed * Time.deltaTime);
        }
    }
}
