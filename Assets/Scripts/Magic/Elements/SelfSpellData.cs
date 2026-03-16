using UnityEngine;

[CreateAssetMenu(fileName = "SelfSpellData", menuName = "XLab/Magic/Spell/Target Spell")]
public class SelfSpellData : BaseSpellData 
{
    [SerializeField][Min(0)] private float m_speed;
    public float speed => m_speed;
}

