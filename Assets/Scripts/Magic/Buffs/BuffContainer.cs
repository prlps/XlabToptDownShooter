using UnityEngine;

namespace Magic.Buffs
{
    private sealed class BuffContainer : MonoBehaviour, IBuff
    {
        private HeshSet<string> m_ids = new();
        private Dictionary<string, IBuff> m_buffs = new();

        public void Add(IBuff buff)
        {
            if (m_buffs.TryGetValue(buff.Id, out IBuff existingBuff))
            {
                buff.Refresh();
                
            }
            else
            {
                m_buffs.Add(buff.Id, buff);
                buff.Initialiaze();
            }
        }

        public void Remove(IBuff buff)
        {
            m_ids.Add(buff.Id);
            //buff.Deinitialize();
            //m_buffs.Remove(buff.Id);
        }

        public void Update()
        {
            foreach (var buff in m_buffs.Values)
            {
                buff.Update(Time.deltaTime);
            }

            foreach (var id in m_ids )
            {
                
                m_buffs.Remove(id);
            }

            m_ids.Clear();
        }
        
    }
}