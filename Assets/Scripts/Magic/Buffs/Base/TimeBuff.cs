using System;
using UnityEngine;

namespace Magic.Buffs.Base
{
    [Serializable]
    public abstract class TimeBuff : BaseBuff, ITimeBuff
    {
        [SerializeField] private float m_duration = 1f;

        public float Duration => m_duration;

        [field: NonSerialized] public float Timer { get; private set; }

        protected TimeBuff()
        {
        }

        protected TimeBuff(string id, Sprite icon, BuffType type, float duration)
            : base(id, icon, type)
        {
            m_duration = duration;
        }

        protected override void OnInitialize()
        {
            Timer = m_duration;
            base.OnInitialize();
        }

        protected override void OnDeinitializing()
        {
            Timer = 0f;
            base.OnDeinitializing();
        }

        public sealed override void Update(float deltaTime)
        {
            if (Timer > 0f)
            {
                OnUpdated(deltaTime);
                Timer -= deltaTime;
            }
            else
            {
                Deinitialize();
            }
        }

        protected virtual void OnUpdated(float deltaTime)
        {
        }
    }
}
