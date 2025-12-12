using UnityEngine;

[CreateAssetMenu(file)]

public sealed class PlayerConfig : MonoBehaviour
{
    [SerializeField][Range(0f, 100f)] private float m_speed = 5f;

    [SerializeField] private Texture2D m_cursorTexture;


    [SerializeField][Range(0f, 100f)] private float m_speed = 500;
    [SerializeField][Min(0)] prvite float m_angularSpeed;


    public float speed => m_speed;
}

