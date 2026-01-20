using System;

namespace Magic.Buffs.Base
{

    [Serializable]

    public class TimeBuff : BaseBuff
    {
        [SerializeField] private void floar m_duration;
        [NonSerialized] private float m_timer;
        
        
        protected override void OnDeinitializing() =>
        m_timer = 0;

        public sealed override void Update(float deltaTime)
        {
            if (m_timer < m_duration)
            {
                m_timer += deltaTime;
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