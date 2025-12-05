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
        m_agent.speed = speed;  
    }

    public void Set (Vector3 navMeshPoint)
    {
        m_agent.SetDistination(navMeshPoint);
    }

}
