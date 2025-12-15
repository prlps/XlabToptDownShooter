using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Players
{
    public class NaveshMouseResolver : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layerMask = ~0;
        [SerializeField] [Min(0)] private float m_raycastDistance = 1000f;
        [SerializeField] [Min(0)] private float m_navMeshSampleMaxDistance = 100f;

        private Camera m_camera;

        private void OnValidate()
        {
            if (!m_camera)
            {
                m_camera = Camera.main;
            }
        }

        public void Initialize(Camera camera)
        {
            m_camera = camera;
        }

        public Vector3? GetNavMeshPoint(Vector3 mousePosition)
        {
            if (m_camera == null)
                return null;

            var ray = m_camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, m_raycastDistance, m_layerMask))
            {
                if (NavMesh.SamplePosition(hit.point, out var navHit, m_navMeshSampleMaxDistance, NavMesh.AllAreas))
                {
                    return navHit.position;
                }
            }

            return null;
        }
    }
}
