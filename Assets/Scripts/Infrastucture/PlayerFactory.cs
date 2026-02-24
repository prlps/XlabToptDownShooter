using Players;
using UnityEngine;
using UnityEngine.UIElements;
using Object = System.Object;

namespace Infrastucture
{
    public interface IPlayerFactorySettings
    {
        public Vector3 Position { get; set; }
    }

    public class PlayerFactory
    {
        private readonly string m_path;
        private PlayerController m_playerPrefab;
        private PlayerController m_playerInstance;
        
        Vector3 IPlayerFactorySettings.position { get; set; }
        
        public PlayerFactory(string path)
        {
            m_path = path;
        }

        public PlayerController Create()
        {
            if (m_playerPrefab is not null)
            {
                return m_playerPrefab;
            }

            if (m_playerPrefab is null)
            {
                var playerPrefab = Resources.Load<GameObject>(m_path);
                m_playerPrefab = playerPrefab.GetComponent<PlayerController>();
            }
            
            m_playerInstance = Object.Instantiate(m_playerPrefab, ((IPlayerFactorySettings)this).position, Quaternion.identity);
            return m_playerInstance; 
        }

    }

        public void Release(PlayerController playerController)
        {
            Object.Destroy(playerController.gameObject);
            m_playerInstance = null;
        }
        
        
    }
}