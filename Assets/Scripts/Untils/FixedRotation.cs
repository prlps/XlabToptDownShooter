using UnityEngine;

namespace Untils
{
    public class FixedRotation : MonoBehaviour
    {
        private Transform m_parent;
        private Vector3 m_worldOffset;
        private Quaternion m_rotation;

        void Start()
        {
            m_parent = transform.parent;

            m_rotation = transform.rotation;
            m_worldOffset = transform.position - (m_parent ? m_parent.position : Vector3.zero);
        }

        void Update()
        {
            if (!m_parent)
            {
                return;
            }

            transform.position = m_parent.position + m_worldOffset;
            transform.rotation = m_rotation;
        }
    }
}