using Magic.Buffs;

namespace Magic.Effects
{
    public class BuffEffect : IEffect

    {
        [SerializeReferenceDropdown]
        [SerializeReference] private IBuff[] m_buffs;
        public void Apply(IEffectable effectable)
        {
            if (effectable is BuffContainer container)
            {
                container.Add(buff.Clone());
            }
        }
    }
}