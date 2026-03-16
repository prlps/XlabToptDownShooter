using System;
using Magic.Buffs;
using UnityEngine;

namespace Magic.Effects
{
    [Serializable]
    public class BuffEffect : IEffect
    {
        [SerializeReference] private IBuff[] m_buffs;

        public void Apply(IEffectable effectable)
        {
            if (effectable is not BuffContainer container || m_buffs == null)
            {
                return;
            }

            foreach (var buff in m_buffs)
            {
                if (buff == null)
                {
                    continue;
                }

                container.Add(buff.Clone());
            }
        }
    }
}
