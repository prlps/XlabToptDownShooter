using UnityEngine;

namespace Magic.Buffs
{
    public interface IBuff
    {
        string Id { get; }
        Sprite Icon { get; }
        BuffType Type { get; }

        void Initialize(BuffContainer container);
        void Deinitialize();

        void Update(float deltaTime);

        IBuff Clone();
    }

    public interface ITimeBuff : IBuff
    {
        float Duration { get; }
        float Timer { get; }
    }
}
