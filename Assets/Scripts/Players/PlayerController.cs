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

        private void SetCursor()
        {
            var texture = m_config.cursorTexture;

            if (texture)
            {
                var hotspot = new Vector2(texture.width / 2f, texture.height / 2f);
                Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
            }
        }

        private void Update()
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var lookPoint = m_playerRotationCalculator.Calculate(mousePosition);
            m_playerMovment.RotateTowards(lookPoint);

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector3? navPoint = m_navMeshMouseResolver.GetNavMeshPoint(mousePosition);

                if (navPoint.HasValue)
                {
                    m_playerMovement.SetDestination(navPoint.Value);
                }
            }
        }
}
