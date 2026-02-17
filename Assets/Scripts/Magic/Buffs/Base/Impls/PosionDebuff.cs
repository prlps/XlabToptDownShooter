using System;
using UnityEngine;

namespace Magic.Buffs.Base.Impls
{
    [Serializable]
    public class PosionDebuff : TimeBuff
    {
        [SerializeField] [Min(0f)] private float m_interval = 1f;
        [SerializeField] [Min(0f)] private float m_damagePerSecond = 2f;

        [NonSerialized] private float m_timer;
        private IHealt m_health;

        public PosionDebuff()
        {
        }

        public PosionDebuff(string id, Sprite icon, BuffType type, float duration, float interval, float damagePerSecond)
            : base(id, icon, type, duration)
        {
            m_interval = interval;
            m_damagePerSecond = damagePerSecond;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            m_health = Container != null ? Container.GetComponent<IHealt>() : null;
        }

        protected override void OnDeinitializing()
        {
            m_timer = 0f;
            m_health = null;
            base.OnDeinitializing();
        }

        protected override void OnUpdated(float deltaTime)
        {
            if (m_health == null)
            {
                Deinitialize();
                return;
            }

            if (m_timer < m_interval)
            {
                m_timer += deltaTime;
                return;
            }

            m_timer = 0f;
            m_health.TakeDamage(m_damagePerSecond);
        }

        public override IBuff Clone() =>
            new PosionDebuff(Id, Icon, Type, Duration, m_interval, m_damagePerSecond);
    }
}
