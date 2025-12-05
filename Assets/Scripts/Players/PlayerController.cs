using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [SerialazeField] private PlayerConfig m_config;
        [SerialazeField] private PlayerMovment m_movement;
        [SerialazeField] private Transform m_targetPosition;

       

        private void OnValidte()
        {
            if (!m_playerMovment)
            {
                m_playerMovment = GetComponent<NavMeshMouseResolver>();
            }
        }

        private void Start()
        {
            m_navMeshMouseResolver.Initialize(Camera.main);
        }



(Mouse.current.rightButton.wasPressedThisFrame)
        {
            Debug.Log("Pressed Mouse");
        }

        
        

    }

    

}
