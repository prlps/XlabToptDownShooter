using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfig m_config;
    [SerializeField] private PlayerMovment m_movement;
    [SerializeField] private Transform m_targetPosition;

    private NavMashMouseResolver m_navMeshMouseResolver;
    private PlayerRotationCalculator m_playerRotationCalculator;

    private void OnValidate()
    {
        if (!m_movement)
        {
            m_movement = GetComponent<PlayerMovment>();
        }
        if (!m_navMeshMouseResolver)
        {
            m_navMeshMouseResolver = GetComponent<NavMashMouseResolver>();
        }
    }

    private void Start()
    {
        var camera = Camera.main;
        m_navMeshMouseResolver.Initialize(camera);
        m_playerRotationCalculator = new PlayerRotationCalculator(camera, transform);
        m_movement.Initialize(m_config.speed, m_config.angularSpeed);

        SetupCursor();
    }

    private void SetupCursor()
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
        m_movement.RotateTowards(lookPoint);

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Vector3? navPoint = m_navMeshMouseResolver.GetNavMeshPoint(mousePosition);
            if (navPoint.HasValue)
            {
                m_movement.SetDestination(navPoint.Value);
            }
        }
    }
}
