using System;
using UnityEngine;

namespace Magic.Buffs.Base
{
    [Serializable]
    public abstract class BaseBuff : IBuff
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public BuffType Type { get; private set; }

        protected BuffContainer Container { get; private set; }

        protected BaseBuff()
        {
        }

        protected BaseBuff(string id, Sprite icon, BuffType type)
        {
            Id = id;
            Icon = icon;
            Type = type;
        }

        public void Initialize(BuffContainer container)
        {
            Container = container;
            OnInitialize();
        }

        protected virtual void OnInitialize()
        {
        }

        public void Deinitialize()
        {
            OnDeinitializing();

            if (Container != null)
            {
                Container.Remove(this);
                Container = null;
            }
        }

        protected virtual void OnDeinitializing()
        {
        }

        public virtual void Update(float deltaTime)
        {
        }

        public abstract IBuff Clone();
    }
}
