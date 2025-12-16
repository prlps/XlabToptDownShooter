using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(NavMeshMouseResolver))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerConfig m_config;
        [SerializeField] private PlayerMovement m_playerMovement;
        [SerializeField] private NavMeshMouseResolver m_mouseResolver;

        private PlayerRotationCalculator m_playerRotationCalculator;

        private void OnValidate()
        {
            if (m_playerMovement == null) m_playerMovement = GetComponent<PlayerMovement>();
            if (m_mouseResolver == null) m_mouseResolver = GetComponent<NavMeshMouseResolver>();
        }

        private void Start()
        {
            var cam = Camera.main;
            if (m_mouseResolver != null && cam != null) m_mouseResolver.Initialize(cam);
            m_playerRotationCalculator = new PlayerRotationCalculator(cam, transform);
            if (m_playerMovement != null && m_config != null) m_playerMovement.Initialize(m_config.Speed, m_config.AngularSpeed);
            SetupCursor();
        }

        private void SetupCursor()
        {
            var texture = m_config != null ? m_config.CursorTexture : null;
            if (texture != null)
            {
                var hotspot = new Vector2(texture.width / 2f, texture.height / 2f);
                Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
            }
        }

        private void Update()
        {
            if (m_playerRotationCalculator == null || m_mouseResolver == null || m_playerMovement == null) return;

            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var lookPoint = m_playerRotationCalculator.Calculate(mousePosition);
            m_playerMovement.RotateTowards(lookPoint);

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector3? navPoint = m_mouseResolver.GetNavMeshPoint(mousePosition);
                if (navPoint.HasValue) m_playerMovement.SetDestination(navPoint.Value);
            }
        }
    }
}
