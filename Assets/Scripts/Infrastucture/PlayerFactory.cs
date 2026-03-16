using Players;
using System;
using UnityEngine;

namespace Infrastucture
{
    public interface IPlayerFactorySettings
    {
        Vector3 position { get; set; }
    }

    public interface IPlayerFactory
    {
        PlayerController Create();
        void Release();
    }

    public sealed class PlayerFactory : IPlayerFactory, IPlayerFactorySettings
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
            if (m_playerInstance != null)
            {
                return m_playerInstance;
            }

            if (m_playerPrefab == null)
            {
                var playerPrefab = Resources.Load<GameObject>(m_path);
                if (playerPrefab == null)
                {
                    throw new InvalidOperationException($"Player prefab was not found by path '{m_path}'");
                }

                m_playerPrefab = playerPrefab.GetComponent<PlayerController>();
                if (m_playerPrefab == null)
                {
                    throw new InvalidOperationException($"PlayerController was not found on prefab '{playerPrefab.name}'");
                }
            }

            m_playerInstance = UnityEngine.Object.Instantiate(m_playerPrefab, ((IPlayerFactorySettings)this).position, Quaternion.identity);

            var camera = Camera.main;
            var mouseResolver = ServiceLocator.Resolved<NavMeshMouseResolver>();
            m_playerInstance.Initialize(camera, mouseResolver);

            return m_playerInstance;
        }

        public void Release()
        {
            if (m_playerInstance == null)
            {
                return;
            }

            UnityEngine.Object.Destroy(m_playerInstance.gameObject);
            m_playerInstance = null;
        }
    }
}
