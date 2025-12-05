using UnityEngine;

[CreateAssetMenu(file)]

public sealed class PlayerConfig : MonoBehaviour
{
    [SerializeField][Range(0f, 100f)] private float m_speed = 5f;

    public float speed => m_speed;
}

