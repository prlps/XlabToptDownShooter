using System;
using Enteties;
using UnityEngine;

namespace Magic.Buffs.Base.Impls
{
    [Serializable]
    public class AccelerationBuff : TimeBuff
    {
        [SerializeField] [Min(0f)] private float m_value = 1f;

        private IAcceleration m_acceleration;

        public AccelerationBuff()
        {
        }

        public AccelerationBuff(string id, Sprite icon, BuffType type, float duration, float value)
            : base(id, icon, type, duration)
        {
            m_value = value;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();
            m_acceleration = Container != null ? Container.GetComponent<IAcceleration>() : null;

            if (m_acceleration == null)
            {
                Deinitialize();
                return;
            }

            m_acceleration.IncreaseAcceleration(m_value);
        }

        protected override void OnDeinitializing()
        {
            if (m_acceleration != null)
            {
                m_acceleration.DecreaseAcceleration(m_value);
                m_acceleration = null;
            }

            base.OnDeinitializing();
        }

        public override IBuff Clone() =>
            new AccelerationBuff(Id, Icon, Type, Duration, m_value);
    }
}
