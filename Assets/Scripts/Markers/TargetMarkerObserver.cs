using Players;
using UnityEngine;

namespace Markers
{
    public sealed class TargetMarkerObserver : MonoBehaviour
    {
        [SerializeField] private TargetMarker m_targetMarker;
        [SerializeField] private PlayerMovement m_playerMovement;

        public void Initialize(PlayerMovement playerMovement)
        {
            Deinitialize();
            m_playerMovement = playerMovement;
            Subscribe();
        }

        private void OnEnable()
        {
            Subscribe();
        }

        private void OnDisable()
        {
            Deinitialize();
        }

        private void Deinitialize()
        {
            if (m_playerMovement != null)
            {
                m_playerMovement.Stopped -= OnPlayerStopped;
                m_playerMovement.DestinationChanged -= OnDestinationChanged;
            }
        }

        private void Subscribe()
        {
            if (m_playerMovement != null)
            {
                m_playerMovement.Stopped -= OnPlayerStopped;
                m_playerMovement.Stopped += OnPlayerStopped;
                m_playerMovement.DestinationChanged -= OnDestinationChanged;
                m_playerMovement.DestinationChanged += OnDestinationChanged;
            }
        }

        private void OnPlayerStopped() =>
            m_targetMarker.Hide();

        private void OnDestinationChanged(Vector3 worldPosition) =>
            m_targetMarker.Show(worldPosition);
    }
}
