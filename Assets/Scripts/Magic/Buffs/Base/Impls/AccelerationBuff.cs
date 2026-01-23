using Enteties;
using Unity.VisualScripting;

namespace Magic.Buffs.Base.Impls
{
    [Serializeble]
    public class AccelerationBuff : TimedBuff
    {
        [SerializeField] private float m_value;

        protected override void OnInitialize()
        {
            base.OnInitialized();
            
            m_acceleration = container.GetComponent<IAcceleration>();

            if (m_acceleration is null)
            {
                Deinitialize();
            }
            else
            {
                {
                    m_acceleration.IncreaseAcceleration(m_value);
                }
            }
        }
        
        public AccelerationBuff(
            srting id,
            float duration,
            float value)
            : base(id, duration)
        {
            m_value = value;
        }
            
        
        public overide IBuff Clone()
            
            
    }
}