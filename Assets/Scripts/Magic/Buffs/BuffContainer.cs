using System;
using System.Collections.Generic;
using UnityEngine;

namespace Magic.Buffs
{
    public sealed class BuffContainer : MonoBehaviour, IEffectable
    {
        private readonly HashSet<string> m_ids = new();
        private readonly Dictionary<string, IBuff> m_buffs = new();

        public event Action<IBuff> BuffAdded;
        public event Action<IBuff> BuffRemoved;

        public IReadOnlyCollection<IBuff> Buffs => m_buffs.Values;

        private void Update()
        {
            foreach (var buff in m_buffs.Values)
            {
                buff.Update(Time.deltaTime);
            }

            foreach (var id in m_ids)
            {
                if (!m_buffs.TryGetValue(id, out var buff))
                {
                    continue;
                }

                m_buffs.Remove(id);
                BuffRemoved?.Invoke(buff);
            }

            m_ids.Clear();
        }

        public void Add(IBuff buff)
        {
            if (buff == null)
            {
                return;
            }

            if (m_buffs.TryGetValue(buff.Id, out var existingBuff))
            {
                existingBuff.Refresh(this);
                return;
            }

            m_buffs.Add(buff.Id, buff);
            buff.Initialize(this);
            BuffAdded?.Invoke(buff);
        }

        public void Remove(IBuff buff)
        {
            if (buff == null)
            {
                return;
            }

            m_ids.Add(buff.Id);
        }
    }
}
