using System;
using UnityEngine;

namespace Magic.Buffs.Base.Impls
{
    [Serializable]
    public class PosionDebuff : TimeBuff
    {
        [SerializeField][Min(0)] private float m_interval = 1;
        [SerializeField][Min(0)] private float m_damagedPerSeconds = 2f;

        [NonSerialized] private float m_timer;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            m_health = container.GetComponent<IHealt>();
        }

        protected override void OnDeinitializing()
        {
            m_timer = 0;
            m_health = 0;
            base.OnDeinitializing();
        }
        
        protected override void OnUpdate(float deltaTime)
        {
            if (m_timer < m_interval)
            {
                m_timer += deltaTime;
            }
            else
            {
                m_timer = 0;
                m_health.TakeDamage(m_damagedPerSeconds);
            }
        }
    }
}