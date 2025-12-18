using UnityEngine;

[CreateAssetMenu(fileName = "AoeSpellData", menuName = "XLab/Magic/Spell/Aoe Spell")]
public class AoeSpellData : BaseSpellData 
{
    [SerializeField][Min(0f)] private float m_radius;

    public float radius => m_radius;

    [SerializeField] private bool m_isTarget;
    public bool isTarget => m_isTarget;
}
