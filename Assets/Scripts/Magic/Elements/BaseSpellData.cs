using UnityEngine;

public abstract class BaseSpellData : MonoBehaviour
{
    [SerializeField] private string m_spellName;
    [SerializeField] private GameObject m_visualEffect;
    [SerializeField] private ElementType[] m_combination;

    [SerialieReferenceDropdown]
    SerializeReference private Ieffect

    [SerializeField] private IEffect[] m_effects;

    public string spellName => m_spellName;

    public GameObject visualEffect => m_visualEffect;

    public IReadOnlyList<ElementType> combination => m_combination;

   private void OnValidate()
    {
        if (m_combination? > 3)
        {
            m_combination = m_combination.Take(3).ToArray();
        }
    }
}
