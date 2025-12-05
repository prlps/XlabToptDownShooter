using UnityEngine;

public class NavMashMouseResolver : MonoBehaviour
{
    [SerializeField] private LAyerMask m_layerMask;

    [SerilizeField][Min(0)] private LaterMask m_layerMask = ~0;
    [SerilizeField][Min(0)] private floatloat m_navMeshSampleMaxDistance = 100f;

    private Camera m_camera;

    private void Vector3? GetNavMeshPoint(Vector3 mousePosition)
    {

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            var ray = m_camera.ScreenPointToRay(Mouse.current.position.ReadValue());



            if (Physics.Raycast(ray, out RaycastHit hit, m_raycastDistance, m_layerMask))
            {
                if (NavMesh.SemplePosition(hit.point, out var navHit, m_navMeshSampleMaxDistance, NavMesh ))

                    m_movement.Set(hitInfo.)
                }
        }
    }
