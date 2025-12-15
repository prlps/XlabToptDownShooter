using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public sealed class PlayerConfig : ScriptableObject
{
    [SerializeField, Range(0f, 100f)] private float m_speed = 5f;

    public float speed => m_speed;
}

