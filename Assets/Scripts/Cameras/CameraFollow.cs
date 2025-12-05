using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform m_target;
    [SerializeField] private Vector3 m_offset = new Vector3(0, 15, -10);
    [SerializeField][Range(0.001f, 1f)] private float m_smootTime = 0.15f;

    private Vector3 m_velosity;

    private void LateUpdate()
    {
        if (!m_target)
        {
            return;
        }

        var targetPosition = m_target.position + m_offset;
        transform.position = Vector3.Smoothdamp(transform.position, targetPosition, ref _velocity, m_smoothTime);
    }
    public void SetTarget(Transform target) =>
        m_target = target;
}
