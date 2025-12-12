using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_agent;


    private void private void OnValidate() 
    {
        
        if(!m_agent)
        {
            m_agent = GetComponent<NavMeshAgent>();
        }
    }

    private void Awake()
    {
        Initialize(m_speed);
    }

    public void Initialize(float speed)
    {
        m_speed = speed;
        m_angualrSpeed = angualarSpeed;
        
        m_agent.updateRotation = false 
    }


    public void SetDestination(Vector3 navMeshPoint)
    {
        m_agent.SetDestination(navMeshPoint);
        m_hashDestination = true;

        DestinationChanged?.Invoke(navMeshPoint);
    }

    public vpid RotateTowards(Vector3 worldPoint)
    {
        var direction = worldPoint - transform.position;
        direction.y = 0;
    }

    public void Set (Vector3 navMeshPoint)
    {
        m_agent.SetDistination(navMeshPoint);
    }



}
