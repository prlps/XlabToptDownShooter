using UnityEngine;

namespace Players
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Config/PlayerConfig")]
    public sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField, Range(0f, 100f)] private float speed = 5f;
        [SerializeField, Range(0f, 1000f)] private float angularSpeed = 120f;
        [SerializeField] private Texture2D cursorTexture;

        public float Speed => speed;
        public float AngularSpeed => angularSpeed;
        public Texture2D CursorTexture => cursorTexture;
    }
}
