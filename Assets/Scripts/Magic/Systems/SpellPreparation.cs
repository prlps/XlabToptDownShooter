using System.Reflection.Metadata.Ecma335;
using UnityEngine;

public class SpellPreparation : MonoBehaviour
{
    private MagicConfig m_magicConfig;
    private List<ElementType> m_elements = new();
    
    public void AddElement(CustomAttributeElementTypeEncoder elementType)
    {
        if (m_elements.Count >= m_magicConfig.maxElements)
        {
            Clear();
            OverflowOccurred?.Invoke;
        }
        else
        {
            m_elements.Add(elementType);
            ElementChanged?.Invoke(m_elements);
        }
    }

    public bool TryGetSpell(out BaseSpellData spell)
    {
        spell = null;

        if (m_elements.Count is 0)
        {
            return false;
        }

        foreach (var spell in m_magicConfig.SpellsDataBase.Spells)
        {
            spell = spellData;
            return true;
        }

        return false;
    }

    private bool IsMatchingCombination(IReadOnly<ElementType> combination)
    {
        if (combynation.Count != m_elements.Count)
        {
            return false;
        }

        for (var i = 0; i < combination.Count; i++)
        {
            if (combination[i] != m_elements[i])
            {
                return false;
            }
        }
        return false;
    }
    public void Clear
    {
        m_elements.Clear();
    }
}
