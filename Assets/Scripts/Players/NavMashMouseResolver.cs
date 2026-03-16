using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Players
{
    public class NavMeshMouseResolver : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layerMask = ~0;
        [SerializeField, Min(0f)] private float m_raycastDistance = 1000f;
        [SerializeField, Min(0f)] private float m_navMeshSampleMaxDistance = 100f;

        private Mouse m_mouse;
        private Camera m_camera;

        private void Awake()
        {
            m_camera = Camera.main;
            m_mouse = Mouse.current;
        }

        private void OnValidate()
        {
            if (m_camera == null) m_camera = Camera.main;
        }

        public void Initialize(Camera camera)
        {
            m_camera = camera;
            if (m_mouse == null) m_mouse = Mouse.current;
        }

        public Vector3 mousePosition
        {
            get
            {
                if (m_mouse == null)
                {
                    m_mouse = Mouse.current;
                }

                return m_mouse != null ? m_mouse.position.ReadValue() : Vector3.zero;
            }
        }

        public Vector3? GetNavMeshPoint(Vector3 mousePosition)
        {
            if (m_camera == null) return null;

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

        public Vector3? GetCursoureWorldPosition()
        {
            if (m_camera == null) return null;

            var ray = m_camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                return hit.point;
            }

            var plane = new Plane(Vector3.up, Vector3.zero);
            if (plane.Raycast(ray, out var distance))
            {
                return ray.GetPoint(distance);
            }

            return null;
        }
    }
}
