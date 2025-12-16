using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovment : MonoBehaviour
{
    public event Action Stopped;
    public event Action<Vector3> DestinationChanged;

    [SerializeField] private NavMeshAgent m_agent;

    private float m_speed;
    private float m_angualrSpeed;
    private bool m_hashDestination;

    private void OnValidate()
    {
        if(!m_agent)
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Awake()
    {
        Initialize(m_speed, m_angualrSpeed);
    }

    public void Initialize(float speed, float angularSpeed)
    {
        m_speed = speed;
        m_agent.speed = speed;
        m_angualrSpeed = angularSpeed;
        m_agent.angularSpeed = angularSpeed;
        m_agent.updateRotation = false;
    }

    public void SetDestination(Vector3 navMeshPoint)
    {
        m_agent.SetDestination(navMeshPoint);
        m_hashDestination = true;
        DestinationChanged?.Invoke(navMeshPoint);
    }

    public void RotateTowards(Vector3 worldPoint)
    {
        var direction = worldPoint - transform.position;
        direction.y =0f;
        if(direction.sqrMagnitude <0.0001f) return;
        var targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, m_agent.angularSpeed * Time.deltaTime);
    }
}
