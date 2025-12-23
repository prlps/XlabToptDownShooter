using System.Numerics;
using System.Threading.Tasks.Dataflow;
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

        m_rotation = transform.m_rotation;
        m_worldOffset = transform.psition - m_parent.position;
        }

        void Update()
        {
            if (!m_parent)
            {
                return;
            }

            transform.psition = m_parent.position + m_worldOffset;
            transform.rotation = m_rotation;
        }
    }
}