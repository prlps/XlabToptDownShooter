using Players;
using UnityEngine;

namespace Markers
{
    public class AIMLineMarker : MonoBehaviour
    {
        [SerializeField] private LineRenderer m_lineRenderer;
        [SerializeField] private NavMeshMouseResolver m_mouseResolver;

        [SerializeField] private float m_zOffset = 0.5f;
        [SerializeField] private float m_lineWidth = 0.1f;
        [SerializeField] private float m_disableDistance = 1f;

        private Transform m_playerTransform;

        private void OnValidate()
        {
            if (!m_lineRenderer)
            {
                m_lineRenderer = GetComponent<LineRenderer>();
            }
        }

        private void Awake()
        {
            m_lineRenderer.positionCount = 2;
            m_lineRenderer.startWidth = m_lineWidth;
            m_lineRenderer.endWidth = m_lineWidth;
        }

        private void LateUpdate()
        {
            if (m_playerTransform == null)
            {
                return;
            }

            var playerPosition = m_playerTransform.position;
            var end = GetAimPosition();

            var direction = (end - playerPosition).normalized;
            var start = playerPosition + direction * m_zOffset;

            start.y = playerPosition.y;
            end.y = playerPosition.y;

            m_lineRenderer.SetPosition(0, start);
            m_lineRenderer.SetPosition(1, end);
            m_lineRenderer.enabled = Vector3.Distance(start, end) > m_disableDistance;
        }

        public void Initialize(Transform playerTransform)
        {
            m_playerTransform = playerTransform;
        }

        private Vector3 GetAimPosition()
        {
            var worldPosition = m_mouseResolver.GetCursoureWorldPosition();

            if (worldPosition.HasValue)
            {
                return worldPosition.Value;
            }

            return m_playerTransform.position + m_playerTransform.forward;
        }
    }
}
