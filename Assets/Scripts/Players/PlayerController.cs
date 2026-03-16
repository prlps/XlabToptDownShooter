using Magic.Systems;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private HealthComponent m_health;
        [SerializeField] private PlayerMovement m_movement;
        [SerializeField] private PlayerConfig m_config;
        [SerializeField] private MagicInputHelper m_inputHelper;
        [SerializeField] private NavMeshMouseResolver m_mouseResolver;

        public PlayerConfig config => m_config;
        public HealthComponent health => m_health;

        private PlayerRotationCalculator m_playerRotationCalculator;
        private bool m_initialized;

        private void OnValidate()
        {
            if (m_movement == null)
            {
                m_movement = GetComponent<PlayerMovement>();
            }

            if (m_mouseResolver == null)
            {
                m_mouseResolver = GetComponent<NavMeshMouseResolver>();
            }

            if (m_health == null)
            {
                m_health = GetComponent<HealthComponent>();
            }
        }

        public void Initialize(Camera camera, NavMeshMouseResolver mouseResolver)
        {
            if (m_initialized)
            {
                return;
            }

            m_mouseResolver = mouseResolver;
            if (m_mouseResolver != null)
            {
                m_mouseResolver.Initialize(camera);
            }

            if (m_movement == null)
            {
                m_movement = GetComponent<PlayerMovement>();
            }

            if (m_health == null)
            {
                m_health = GetComponent<HealthComponent>() ?? gameObject.AddComponent<HealthComponent>();
            }

            if (m_config != null)
            {
                m_movement.Initialize(m_config.Speed, m_config.AngularSpeed);
                m_health.Initialize(m_config.Health);
            }

            m_playerRotationCalculator = new PlayerRotationCalculator(camera, transform);
            SetupCursor();

            if (m_inputHelper != null)
            {
                var magicSystem = GetComponent<MagicSystem>();
                if (magicSystem != null)
                {
                    m_inputHelper.Bind(magicSystem);
                }
            }

            m_initialized = true;
        }

        private void Start()
        {
            if (!m_initialized)
            {
                Initialize(Camera.main, m_mouseResolver);
            }
        }

        private void Update()
        {
            if (!m_initialized || m_playerRotationCalculator == null || m_mouseResolver == null)
            {
                return;
            }

            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var lookPoint = m_playerRotationCalculator.Calculate(mousePosition);
            m_movement.RotateTowards(lookPoint);

            if (Mouse.current.rightButton.wasPressedThisFrame)
            {
                Vector3? navPoint = m_mouseResolver.GetNavMeshPoint(mousePosition);

                if (navPoint.HasValue)
                {
                    m_movement.SetDestination(navPoint.Value);
                }
            }

            m_inputHelper?.Update();
        }

        private void SetupCursor()
        {
            var texture = m_config != null ? m_config.CursorTexture : null;
            if (texture == null)
            {
                return;
            }

            var hotspot = new Vector2(texture.width / 2f, texture.height / 2f);
            Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
        }
    }
}
