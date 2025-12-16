using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField, Range(0f, 100f)] private float m_speed = 5f;
        [SerializeField] private Texture2D m_cursorTexture;
        [SerializeField, Min(0f)] private float m_angularSpeed = 500f;

        public float Speed => m_speed;
        public Texture2D CursorTexture => m_cursorTexture;
        public float AngularSpeed => m_angularSpeed;
    }
}
