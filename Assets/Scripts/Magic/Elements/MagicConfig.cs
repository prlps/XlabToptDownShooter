using UnityEngine;

public sealed class MagicConfig : ScriptableObject
{
    [SerializeField] private ElementData m_elementsData;
    [SerializeField] private SpellDataBase m_spellDataBase;

    [SerializeField][Min(1)] private int m_maxElements =3;
    [SerializeField][Min(0f)] private float m_cancelCooldown =0.3f;

    public ElementData ElementData => m_elementsData;

    public SpellDataBase SpellDataBase => m_spellDataBase;

    public int MaxElements => m_maxElements;

    public float cancelCooldown => m_cancelCooldown;
}
