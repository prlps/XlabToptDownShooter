using UnityEngine;

namespcae Magic.Spells.Data
{
    [CreateAssetMenu(fileName = "TargetSpellData", menuName = "XLab/Magic/Spell/Target Spell")]

    public class TargetSpellData : BaseSpellData 
    {
    [SerializeField][Min(0)] private float m_speed;

    public float speed => m_speed;
    }
}