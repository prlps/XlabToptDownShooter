using UnityEngine;

[CreateAssetMenu(fileName = "TargetSpellData", menuName = "XLab/Magic/Spell/Target Spell")]
public class TargetSpellData : BaseSpellData
{
    [SerializeField][Min(0f)] private float m_speed;

    public float speed => m_speed;
}