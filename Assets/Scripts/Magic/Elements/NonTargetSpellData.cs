using UnityEngine;

[CreateAssetMenu(fileName = "NonTargetSpellData", menuName = "XLab/Magic/Spell/NonTarget Spell")]
public class NonTargetSpellData : BaseSpellData
{
 [SerializeField][Min(0f)] private float m_cooldown;

 public float cooldown => m_cooldown;
}
