using UnityEngine;

[CreateAssetMenu(fileName = "TargetAoeSpellData", menuName = "XLab/Magic/Spell/Target AOE Spell")]
public class TargetAoeSpellData : BaseSpellData
{
    [SerializeField][Min(0f)] private float m_radius;

    public float radius => m_radius;
}
