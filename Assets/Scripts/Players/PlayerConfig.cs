using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [field: SerializeField]
        [field: Range(0f, 100f)]
        public float m_speed { get; set; } = 5f;
    }
}

