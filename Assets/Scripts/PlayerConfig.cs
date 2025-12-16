using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public sealed class PlayerConfig : ScriptableObject
{
    [SerializeField, Range(0f,100f)] private float m_speed =5f;
    [SerializeField] private Texture2D m_cursorTexture;
    [SerializeField, Min(0f)] private float m_angularSpeed =500f;

    public float speed => m_speed;
    public Texture2D cursorTexture => m_cursorTexture;
    public float angularSpeed => m_angularSpeed;
}

